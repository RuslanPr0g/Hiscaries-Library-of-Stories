import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { take, takeUntil } from 'rxjs';
import { DestroyService } from '../../shared/services/destroy.service';
import { StoryModel } from '../models/domain/story-model';
import { Store } from '@ngrx/store';
import { searchStoryByTerm } from '../store/story.actions';
import { StoryStateModel } from '../store/story-state.model';
import { SearchStoryResultsComponent } from '../search-story-results/search-story-results.component';
import { TemplateMessageModel } from '../models/template-message.model';
import { StoryWithMetadataService } from '../../user-to-story/services/multiple-services-merged/story-with-metadata.service';

@Component({
    selector: 'app-search-story',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './search-story.component.html',
    styleUrl: './search-story.component.scss',
    providers: [DestroyService],
})
export class SearchStoryComponent implements OnInit {
    stories: StoryModel[] = [];
    errorMessage: TemplateMessageModel | null;

    isLoading = false;

    constructor(
        private store: Store<StoryStateModel>,
        private destroy: DestroyService,
        private route: ActivatedRoute,
        private storyService: StoryWithMetadataService
    ) {}

    ngOnInit(): void {
        this.route.paramMap.pipe(takeUntil(this.destroy.subject$)).subscribe((params) => {
            const searchTerm = params.get('term');
            this.searchStoryByTerm(searchTerm);
        });
    }

    get storiesLoaded(): boolean {
        return this.stories && this.stories.length > 0;
    }

    private searchStoryByTerm(term: string | null): void {
        if (!term) {
            return;
        }

        this.store.dispatch(searchStoryByTerm({ SearchTerm: term }));

        this.stories = [];
        this.isLoading = true;

        this.storyService
            .searchStory({
                SearchTerm: term,
            })
            .pipe(take(1))
            .subscribe({
                next: (stories: StoryModel[]) => {
                    this.stories = stories;

                    if (stories?.length === 0) {
                        this.errorMessage = {
                            Title: 'Search criteria',
                            Description: 'No stories found by the criteria',
                        };
                    } else {
                        this.errorMessage = null;
                    }

                    this.isLoading = false;
                },
            });
    }
}
