import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoryService } from '../services/story.service';
import { CommonModule } from '@angular/common';
import { take, takeUntil } from 'rxjs';
import { DestroyService } from '../../shared/services/destroy.service';
import { StoryModel } from '../models/domain/story-model';

@Component({
    selector: 'app-search-story',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './search-story.component.html',
    styleUrl: './search-story.component.scss',
    providers: [DestroyService],
})
export class SearchStoryComponent implements OnInit {
    stories: StoryModel[] = [];

    searchTerm: string | null;

    storyNotFound: boolean = false;

    constructor(
        private destroy: DestroyService,
        private route: ActivatedRoute,
        private storyService: StoryService
    ) {}

    ngOnInit(): void {
        this.route.paramMap.pipe(takeUntil(this.destroy.subject$)).subscribe((params) => {
            const searchTerm = params.get('term');
            this.searchStoryByTerm(searchTerm);
            this.searchTerm = searchTerm;
        });
    }

    private searchStoryByTerm(term: string | null): void {
        if (!term) {
            return;
        }

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
