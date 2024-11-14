import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DestroyService } from '../../shared/services/destroy.service';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { StoryModel } from '../models/domain/story-model';
import { SearchStoryResultsComponent } from '../search-story-results/search-story-results.component';

@Component({
    selector: 'app-search-story-resume-reading',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './search-story-resume-reading.component.html',
    styleUrl: './search-story-resume-reading.component.scss',
    providers: [DestroyService],
})
export class SearchStoryResumeReadingComponent implements OnInit {
    private _stories: StoryModel[] = [];

    isLoading: boolean = false;

    constructor(private storyService: StoryService) {}

    ngOnInit(): void {
        this.fetchStories();
    }

    get displayedStories(): StoryModel[] {
        return this._stories;
    }

    private fetchStories(shouldSetLoading: boolean = true): void {
        this.isLoading = shouldSetLoading;

        this.storyService
            .resumeReading()
            .pipe(take(1))
            .subscribe((stories) => {
                this._stories = stories;
                this.isLoading = false;
            });
    }
}
