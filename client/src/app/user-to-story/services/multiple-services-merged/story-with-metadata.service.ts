import { Injectable } from '@angular/core';
import { Observable, switchMap, map, catchError, of } from 'rxjs';
import { BaseIdModel } from '../../../shared/models/base-id.model';
import { GenreModel } from '../../../stories/models/domain/genre.model';
import { StoryModel, StoryModelWithContents } from '../../../stories/models/domain/story-model';
import { ModifyStoryRequest } from '../../../stories/models/requests/modify-story.model';
import { PublishStoryRequest } from '../../../stories/models/requests/publish-story.model';
import { SearchStoryWithContentsRequest } from '../../../stories/models/requests/search-story-with-contents.model';
import { SearchStoryRequest } from '../../../stories/models/requests/search-story.model';
import { StoryService } from '../../../stories/services/story.service';
import { UserService } from '../../../users/services/user.service';
import { SearchStoryByIdsRequest } from '../../../stories/models/requests/story-by-ids.model';

@Injectable({
    providedIn: 'root',
})
export class StoryWithMetadataService {
    constructor(
        private storyService: StoryService,
        private userService: UserService
    ) {}

    genreList(): Observable<GenreModel[]> {
        return this.storyService.genreList();
    }

    recommendations(): Observable<StoryModel[]> {
        return this.enrichStories(this.storyService.recommendations());
    }

    getStoriesByLibraryId(libraryId: string): Observable<StoryModel[]> {
        return this.enrichStories(this.storyService.getStoriesByLibraryId(libraryId));
    }

    searchStory(request: SearchStoryRequest): Observable<StoryModel[]> {
        return this.enrichStories(this.storyService.searchStory(request));
    }

    getStoryByIdWithContents(request: SearchStoryWithContentsRequest): Observable<StoryModelWithContents> {
        return this.enrichStory(this.storyService.getStoryByIdWithContents(request));
    }

    getStoriesByIds(request: SearchStoryByIdsRequest): Observable<StoryModel[]> {
        return this.enrichStories(this.storyService.getStoriesByIds(request));
    }

    publish(request: PublishStoryRequest): Observable<BaseIdModel> {
        return this.storyService.publish(request);
    }

    modify(request: ModifyStoryRequest): Observable<void> {
        return this.storyService.modify(request);
    }

    private enrichStories(source$: Observable<StoryModel[]>): Observable<StoryModel[]> {
        return this.enrich(source$) as Observable<StoryModel[]>;
    }

    private enrichStory(source$: Observable<StoryModelWithContents>): Observable<StoryModelWithContents> {
        return this.enrich(source$) as Observable<StoryModelWithContents>;
    }

    private enrich<T extends StoryModel>(source$: Observable<T | T[]>): Observable<T | T[] | null> {
        return source$.pipe(
            switchMap((input) => {
                if (!input) {
                    return of(input);
                }

                const isArray = Array.isArray(input);
                const stories = isArray ? (input as T[]) : [input as T];

                if (stories.length === 0) {
                    return of(input);
                }

                const metadataRequest = {
                    Items: stories.map((story) => ({
                        StoryId: story.Id,
                        LibraryId: story.LibraryId,
                        TotalPages: story.TotalPages,
                    })),
                };

                return this.userService.getUserReadingStoryMetadata(metadataRequest).pipe(
                    map((metadataList) => {
                        if (!metadataList || metadataList.length === 0) {
                            return input;
                        }

                        const metadataMap = new Map(metadataList.map((meta) => [meta.StoryId, meta]));
                        const enriched = stories.map((story) => {
                            const metadata = metadataMap.get(story.Id);
                            return metadata ? { ...story, ...metadata } : story;
                        });

                        return isArray ? enriched : enriched[0];
                    }),
                    catchError((error) => {
                        console.error('Failed to fetch story metadata', error);
                        return of(input);
                    })
                );
            })
        );
    }
}
