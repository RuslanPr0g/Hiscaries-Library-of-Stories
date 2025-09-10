import { Component, OnInit, inject, effect, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { finalize } from 'rxjs';
import { DestroyService } from '@shared/services/destroy.service';
import { StoryModel } from '@stories/models/domain/story-model';
import { Store } from '@ngrx/store';
import { StoryStateModel } from '@stories/store/story-state.model';
import { SearchStoryResultsComponent } from '@stories/search-story-results/search-story-results.component';
import { TemplateMessageModel } from '@stories/models/template-message.model';
import { StoryWithMetadataService } from '@user-to-story/services/multiple-services-merged/story-with-metadata.service';
import { emptyQueriedResult, QueriedModel } from '@shared/models/queried.model';
import { PaginationService } from '@shared/services/statefull/pagination.service';

@Component({
    selector: 'app-search-story',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './search-story.component.html',
    styleUrls: ['./search-story.component.scss'],
    providers: [DestroyService, PaginationService],
})
export class SearchStoryComponent implements OnInit {
    private storyService = inject(StoryWithMetadataService);
    private pagination = inject(PaginationService);
    private destroy = inject(DestroyService);
    private store = inject(Store<StoryStateModel>);
    private route = inject(ActivatedRoute);

    stories = signal<QueriedModel<StoryModel>>(emptyQueriedResult);
    errorMessage = signal<TemplateMessageModel | null>(null);
    isLoading = this.pagination.isLoading;

    ngOnInit(): void {
        effect(() => {
            const term = this.route.snapshot.paramMap.get('term');
            if (!term) return;

            const query = this.pagination.snapshot;
            this.pagination.setLoading(true);

            this.storyService
                .searchStory({ SearchTerm: term, QueryableModel: query })
                .pipe(finalize(() => this.pagination.setLoading(false)))
                .subscribe((data) => {
                    this.stories.set(data);
                    this.errorMessage.set(
                        data.Items.length === 0
                            ? { Title: 'Search criteria', Description: 'No stories found by the criteria' }
                            : null
                    );
                });
        });
    }

    nextPage() {
        this.pagination.nextPage();
    }

    prevPage() {
        this.pagination.prevPage();
    }

    get storiesLoaded(): boolean {
        return this.stories().Items.length > 0;
    }
}
