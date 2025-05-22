import { Injectable } from '@angular/core';
import { Observable, switchMap, map } from 'rxjs';
import { BaseIdModel } from '../../../shared/models/base-id.model';
import { GenreModel } from '../../../stories/models/domain/genre.model';
import { StoryModel, StoryModelWithContents } from '../../../stories/models/domain/story-model';
import { ModifyStoryRequest } from '../../../stories/models/requests/modify-story.model';
import { PublishStoryRequest } from '../../../stories/models/requests/publish-story.model';
import { SearchStoryWithContentsRequest } from '../../../stories/models/requests/search-story-with-contents.model';
import { SearchStoryRequest } from '../../../stories/models/requests/search-story.model';
import { StoryService } from '../../../stories/services/story.service';
import { UserService } from '../../../users/services/user.service';

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

    resumeReading(): Observable<StoryModel[]> {
        return this.enrichStories(this.storyService.resumeReading());
    }

    readingHistory(): Observable<StoryModel[]> {
        return this.enrichStories(this.storyService.readingHistory());
    }

    searchStory(request: SearchStoryRequest): Observable<StoryModel[]> {
        return this.enrichStories(this.storyService.searchStory(request));
    }

    getStoryByIdWithContents(request: SearchStoryWithContentsRequest): Observable<StoryModelWithContents> {
        return this.enrichStory(this.storyService.getStoryByIdWithContents(request));
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

    private enrich<T extends StoryModel>(source$: Observable<T | T[]>): Observable<T | T[]> {
        return source$.pipe(
            switchMap((input) => {
                const isArray = Array.isArray(input);
                const stories = isArray ? (input as T[]) : [input as T];
                const metadataRequest = {
                    Items: stories.map((story) => ({
                        StoryId: story.Id,
                        LibraryId: story.LibraryId,
                        TotalPages: story.TotalPages,
                    })),
                };

                return this.userService.getUserReadingStoryMetadata(metadataRequest).pipe(
                    map((metadataList) => {
                        const metadataMap = new Map(metadataList.map((meta) => [meta.StoryId, meta]));
                        const enriched = stories.map((story) => {
                            const metadata = metadataMap.get(story.Id);
                            return metadata ? { ...story, ...metadata } : story;
                        });

                        return isArray ? enriched : enriched[0];
                    })
                );
            })
        );
    }
}
