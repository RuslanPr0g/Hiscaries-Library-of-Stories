import { Component, inject, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { switchMap, finalize } from 'rxjs';
import { SearchStoryResultsComponent } from '@stories/search-story-results/search-story-results.component';
import { UserStoryService } from '@user-to-story/services/multiple-services-merged/user-story.service';
import { PaginationService } from '@shared/services/statefull/pagination.service';
import { emptyQueriedResult } from '@shared/models/queried.model';

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
    private pagination = inject(PaginationService);
    empty = emptyQueriedResult;

    stories$ = this.pagination.query().pipe(
        switchMap((q) => {
            this.pagination.setLoading(true);
            return this.userStoryService.resumeReading(q).pipe(finalize(() => this.pagination.setLoading(false)));
        })
    );

    isLoading$ = this.pagination.isLoading();

    constructor() {
        effect(() => {
            const q = this.pagination.query();
            this.pagination.setLoading(true);

            this.userStoryService.resumeReading(q).subscribe({
                next: (res) => {
                    this.pagination.setLoading(false);
                },
                error: () => {
                    this.pagination.setLoading(false);
                },
            });
        });
    }

    nextPage() {
        this.pagination.nextPage();
    }

    prevPage() {
        this.pagination.prevPage();
    }
}
