import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { switchMap, finalize, tap } from 'rxjs';
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
    pagination = inject(PaginationService);
    empty = emptyQueriedResult;

    stories$ = this.pagination.query$.pipe(
        // tap(() => this.pagination.setLoading(true)),
        switchMap((q) => {
            this.pagination.setLoading(true);
            return this.userStoryService.resumeReading(q).pipe(finalize(() => this.pagination.setLoading(false)));
        })
    );

    isLoading$ = this.pagination.isLoading$;

    nextPage() {
        this.pagination.nextPage();
    }

    prevPage() {
        this.pagination.prevPage();
    }
}
