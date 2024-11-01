import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { StoryModel } from '../models/domain/story-model';
import { SearchStoryItemComponent } from '../story-search-item/story-search-item.component';
import { SkeletonModule } from 'primeng/skeleton';
import { CarouselModule, CarouselResponsiveOptions } from 'primeng/carousel';
import { ButtonOneComponent } from '../../shared/components/button-one/button-one.component';

@Component({
    selector: 'app-search-story-results',
    standalone: true,
    imports: [CommonModule, SearchStoryItemComponent, SkeletonModule, CarouselModule, ButtonOneComponent],
    templateUrl: './search-story-results.component.html',
    styleUrls: ['./search-story-results.component.scss'],
})
export class SearchStoryResultsComponent implements OnInit {
    private _stories: StoryModel[] = [];
    private readonly _chunkSize: number = 6;
    private _isCooldown: boolean = false;
    private _storiesLoaded: boolean = false;

    @Input() set initialStories(value: StoryModel[]) {
        this._stories = value;
        this._storiesLoaded = true;
    }

    @Input() isCarousel: boolean = false;

    responsiveOptions: CarouselResponsiveOptions[] | undefined;

    constructor(private storyService: StoryService) {}

    ngOnInit(): void {
        if (this._stories.length === 0 && !this._storiesLoaded) {
            this.fetchRecommendations();
        }

        this.initializeResponsiveOptions();
    }

    get shouldShowMoreButton(): boolean {
        return this.displayedStories && this.displayedStories.length > 0 && !this._storiesLoaded;
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
            this.fetchRecommendations();
        }

        setTimeout(() => {
            this._isCooldown = false;
        }, 3000);

        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    private fetchRecommendations(): void {
        this.storyService
            .recommendations()
            .pipe(take(1))
            .subscribe((stories) => {
                this._stories = [...this._stories, ...stories];
            });
    }

    private initializeResponsiveOptions(): void {
        this.responsiveOptions = [
            {
                breakpoint: '1950px',
                numVisible: 2,
                numScroll: 1,
            },
            {
                breakpoint: '1150px',
                numVisible: 1,
                numScroll: 1,
            },
            {
                breakpoint: '767px',
                numVisible: 1,
                numScroll: 1,
            },
        ];
    }
}
