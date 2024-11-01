import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoryService } from '../services/story.service';
import { CommonModule } from '@angular/common';
import { take, takeUntil } from 'rxjs';
import { DestroyService } from '../../shared/services/destroy.service';
import { StoryModel } from '../models/domain/story-model';
import { Store } from '@ngrx/store';
import { searchStoryByTerm } from '../store/story.actions';
import { StoryStateModel } from '../store/story-state.model';
import { SearchStoryResultsComponent } from '../search-story-results/search-story-results.component';

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

    storyNotFound: boolean = false;

    constructor(
        private store: Store<StoryStateModel>,
        private destroy: DestroyService,
        private route: ActivatedRoute,
        private storyService: StoryService
    ) {}

    ngOnInit(): void {
        this.route.paramMap.pipe(takeUntil(this.destroy.subject$)).subscribe((params) => {
            const searchTerm = params.get('term');
            this.searchStoryByTerm(searchTerm);
        });
    }

    private searchStoryByTerm(term: string | null): void {
        if (!term) {
            return;
        }

        this.store.dispatch(searchStoryByTerm({ SearchTerm: term }));

        this.storyService
            .searchStory({
                SearchTerm: term,
            })
            .pipe(take(1))
            .subscribe({
                next: (stories: StoryModel[]) => {
                    this.stories = stories;
                },
            });
    }
}
