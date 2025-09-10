import { Component, inject, effect, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { finalize } from 'rxjs';
import { SearchStoryResultsComponent } from '@stories/search-story-results/search-story-results.component';
import { UserStoryService } from '@user-to-story/services/multiple-services-merged/user-story.service';
import { PaginationService } from '@shared/services/statefull/pagination.service';
import { StoryModel } from '@stories/models/domain/story-model';
import { emptyQueriedResult, QueriedModel } from '@shared/models/queried.model';

@Component({
    selector: 'app-search-story-resume-reading',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './search-story-resume-reading.component.html',
    styleUrls: ['./search-story-resume-reading.component.scss'],
    providers: [PaginationService],
})
export class SearchStoryResumeReadingComponent {
    private userStoryService = inject(UserStoryService);
    pagination = inject(PaginationService);

    stories = signal<QueriedModel<StoryModel>>(emptyQueriedResult);

    constructor() {
        effect(() => {
            const q = this.pagination.query();
            this.pagination.setLoading(true);
            this.userStoryService
                .resumeReading(q)
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
