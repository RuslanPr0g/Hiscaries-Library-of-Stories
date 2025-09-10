import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { finalize } from 'rxjs';
import { SearchStoryResultsComponent } from '@stories/search-story-results/search-story-results.component';
import { StoryWithMetadataService } from '@user-to-story/services/multiple-services-merged/story-with-metadata.service';
import { PaginationService } from '@shared/services/statefull/pagination.service';
import { ButtonOneComponent } from '@shared/components/button-one/button-one.component';
import { StoryModel } from '@stories/models/domain/story-model';
import { emptyQueriedResult, QueriedModel } from '@shared/models/queried.model';

@Component({
    selector: 'app-search-story-recommendations',
    standalone: true,
    imports: [CommonModule, ButtonOneComponent, SearchStoryResultsComponent],
    templateUrl: './search-story-recommendations.component.html',
    styleUrls: ['./search-story-recommendations.component.scss'],
    providers: [PaginationService],
})
export class SearchStoryRecommendationsComponent {
    private storyService = inject(StoryWithMetadataService);
    pagination = inject(PaginationService);
    stories = signal<QueriedModel<StoryModel>>(emptyQueriedResult);
    isLoading = signal(false);

    constructor() {
        this.loadStories();
    }

    private loadStories() {
        this.isLoading.set(true);
        this.storyService
            .recommendations(this.pagination.snapshot)
            .pipe(finalize(() => this.isLoading.set(false)))
            .subscribe((data) => this.stories.set(data));
    }

    nextPage() {
        this.pagination.nextPage();
        this.loadStories();
    }

    prevPage() {
        this.pagination.prevPage();
        this.loadStories();
    }
}
