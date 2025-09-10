import { Component, inject, effect, signal } from '@angular/core';
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

    constructor() {
        effect(() => {
            const q = this.pagination.query();
            this.pagination.setLoading(true);
            this.storyService
                .recommendations(q)
                .pipe(finalize(() => this.pagination.setLoading(false)))
                .subscribe((data) => this.stories.set(data));
        });
    }

    isLoading = this.pagination.isLoading;

    nextPage() {
        this.pagination.nextPage();
    }

    prevPage() {
        this.pagination.prevPage();
    }
}
