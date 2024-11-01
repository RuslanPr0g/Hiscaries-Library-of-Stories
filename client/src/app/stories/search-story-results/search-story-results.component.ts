import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { StoryModel } from '../models/domain/story-model';
import { SearchStoryItemComponent } from '../story-search-item/story-search-item.component';
import { SkeletonModule } from 'primeng/skeleton';
import { CarouselModule } from 'primeng/carousel';
import { convertToBase64 } from '../../shared/helpers/image.helper';

@Component({
    selector: 'app-search-story-results',
    standalone: true,
    imports: [CommonModule, SearchStoryItemComponent, SkeletonModule, CarouselModule],
    templateUrl: './search-story-results.component.html',
    styleUrl: './search-story-results.component.scss',
})
export class SearchStoryResultsComponent {
    stories: StoryModel[] = [];

    @Input() initialStories: StoryModel[];
    @Input() isCarousel: boolean = false;

    responsiveOptions: any[] | undefined;

    constructor(private storyService: StoryService) {
        if (!this.initialStories || this.initialStories.length === 0) {
            this.storyService
                .recommendations()
                .pipe(take(1))
                .subscribe((stories) => {
                    this.stories = stories;
                });
        }

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
