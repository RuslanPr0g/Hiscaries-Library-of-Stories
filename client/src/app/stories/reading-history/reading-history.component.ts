import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { take } from 'rxjs';
import { DestroyService } from '../../shared/services/destroy.service';
import { StoryModel } from '../models/domain/story-model';
import { SearchStoryResultsComponent } from '../search-story-results/search-story-results.component';
import { UserStoryService } from '../../user-to-story/services/multiple-services-merged/user-story.service';

@Component({
    selector: 'app-reading-history',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './reading-history.component.html',
    styleUrl: './reading-history.component.scss',
    providers: [DestroyService],
})
export class ReadingHistoryComponent implements OnInit {
    stories: StoryModel[] = [];
    isLoading = false;

    constructor(private userStoryService: UserStoryService) {}

    ngOnInit(): void {
        this.fetchStories();
    }

    get storiesLoaded(): boolean {
        return this.stories && this.stories.length > 0;
    }

    private fetchStories(): void {
        this.stories = [];
        this.isLoading = true;

        this.userStoryService
            .readingHistory()
            .pipe(take(1))
            .subscribe({
                next: (stories: StoryModel[]) => {
                    this.stories = stories;
                    this.isLoading = false;
                },
            });
    }
}
