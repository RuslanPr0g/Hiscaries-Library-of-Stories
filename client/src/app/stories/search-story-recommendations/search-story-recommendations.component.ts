import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DestroyService } from '../../shared/services/destroy.service';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { StoryModel } from '../models/domain/story-model';
import { ButtonOneComponent } from '../../shared/components/button-one/button-one.component';
import { SearchStoryResultsComponent } from '../search-story-results/search-story-results.component';

@Component({
    selector: 'app-search-story-recommendations',
    standalone: true,
    imports: [CommonModule, ButtonOneComponent, SearchStoryResultsComponent],
    templateUrl: './search-story-recommendations.component.html',
    styleUrl: './search-story-recommendations.component.scss',
    providers: [DestroyService],
})
export class SearchStoryRecommendationsComponent implements OnInit {
    private _stories: StoryModel[] = [];
    private readonly _chunkSize: number = 6;
    private _isCooldown: boolean = false;

    isLoading: boolean = false;

    constructor(private storyService: StoryService) {}

    ngOnInit(): void {
        this.fetchRecommendations();
    }

    get shouldShowMoreButton(): boolean {
        return this.displayedStories && this.displayedStories.length > 0;
    }

    get isCooldownActive(): boolean {
        return this._isCooldown;
    }

    get displayedStories(): StoryModel[] {
        return this._stories.slice(0, this._chunkSize);
    }

    showMore(): void {
        if (this._isCooldown) {
            return;
        }

        this._isCooldown = true;

        this._stories.splice(0, this._chunkSize);

        if (this._stories.length < 12) {
            this.fetchRecommendations(false);
        }

        setTimeout(() => {
            this._isCooldown = false;
        }, 3000);

        // TODO: we need this on mobile view,
        // + we need to scroll only to the beginning of the current component, not the whole page
        // window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    private fetchRecommendations(shouldSetLoading: boolean = true): void {
        this.isLoading = shouldSetLoading;

        this.storyService
            .recommendations()
            .pipe(take(1))
            .subscribe((stories) => {
                this._stories = [...this._stories, ...stories];
                this.isLoading = false;
            });
    }
}
