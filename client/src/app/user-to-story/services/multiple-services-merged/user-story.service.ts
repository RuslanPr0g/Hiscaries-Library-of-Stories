import { Injectable } from '@angular/core';
import { Observable, of, switchMap, catchError } from 'rxjs';
import { StoryModel } from '../../../stories/models/domain/story-model';
import { UserService } from '../../../users/services/user.service';
import { StoryWithMetadataService } from './story-with-metadata.service';

@Injectable({
    providedIn: 'root',
})
export class UserStoryService {
    constructor(
        private storyService: StoryWithMetadataService,
        private userService: UserService
    ) {}

    resumeReading(): Observable<StoryModel[]> {
        return this.loadStoriesFromIds(() => this.userService.resumeReading(), 'LastPageRead');
    }

    readingHistory(): Observable<StoryModel[]> {
        return this.loadStoriesFromIds(() => this.userService.readingHistory(), 'EditedAt');
    }

    private loadStoriesFromIds(idsSource: () => Observable<string[]>, propertyName: string): Observable<StoryModel[]> {
        if (!idsSource) {
            return of([]);
        }

        try {
            return idsSource().pipe(
                switchMap((ids) => {
                    if (
                        !Array.isArray(ids) ||
                        ids.length === 0 ||
                        ids.some((id) => typeof id !== 'string' || !id.trim())
                    ) {
                        return of([]);
                    }

                    try {
                        return this.storyService
                            .getStoriesByIds({
                                Ids: ids,
                                Sorting: {
                                    Property: propertyName,
                                    Ascending: false,
                                },
                            })
                            .pipe(catchError(() => of([])));
                    } catch {
                        return of([]);
                    }
                }),
                catchError(() => of([]))
            );
        } catch {
            return of([]);
        }
    }
}
