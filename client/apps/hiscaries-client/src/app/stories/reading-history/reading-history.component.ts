import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { switchMap, finalize, tap } from 'rxjs';
import { SearchStoryResultsComponent } from '@stories/search-story-results/search-story-results.component';
import { UserStoryService } from '@user-to-story/services/multiple-services-merged/user-story.service';
import { PaginationService } from '@shared/services/statefull/pagination.service';

@Component({
    selector: 'app-reading-history',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './reading-history.component.html',
    styleUrls: ['./reading-history.component.scss'],
    providers: [PaginationService],
})
export class ReadingHistoryComponent {
    private userStoryService = inject(UserStoryService);
    pagination = inject(PaginationService);

    stories$ = this.pagination.query$.pipe(
        // tap(() => this.pagination.setLoading(true)),
        switchMap((q) => {
            return this.userStoryService.readingHistory(q).pipe(finalize(() => this.pagination.setLoading(false)));
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
