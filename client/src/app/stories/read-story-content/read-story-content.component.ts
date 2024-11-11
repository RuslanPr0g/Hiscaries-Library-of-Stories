import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoryModelWithContents } from '../models/domain/story-model';
import { CommonModule } from '@angular/common';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { convertToBase64 } from '../../shared/helpers/image.helper';
import { IteratorService } from '../../shared/services/iterator.service';
import { ButtonModule } from 'primeng/button';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
    selector: 'app-read-story-content',
    standalone: true,
    imports: [CommonModule, ButtonModule, ProgressSpinnerModule],
    providers: [IteratorService],
    templateUrl: './read-story-content.component.html',
    styleUrl: './read-story-content.component.scss',
})
export class ReadStoryContentComponent implements OnInit {
    storyId: string | null = null;

    globalError: string | null = null;
    story: StoryModelWithContents | null = null;
    storyNotFound: boolean = false;

    maximized: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private storyService: StoryService,
        private iterator: IteratorService
    ) {
        this.storyId = this.route.snapshot.paramMap.get('id');
    }

    ngOnInit() {
        if (!this.storyId) {
            this.storyNotFound = true;
            return;
        }

        this.storyService
            .getStoryByIdWithContents({
                Id: this.storyId,
            })
            .pipe(take(1))
            .subscribe({
                next: (story) => {
                    if (!story) {
                        this.storyNotFound = true;
                        return;
                    }

                    this.story = {
                        ...story,
                        ImagePreviewUrl: convertToBase64(story.ImagePreviewUrl),
                    };

                    this.iterator.upperBoundary = story.Contents.length - 1;

                    this.storyService
                        .read({
                            StoryId: story.Id,
                            PageRead: 0,
                        })
                        .subscribe();
                },
                error: () => (this.storyNotFound = true),
            });
    }

    @HostListener('document:keydown', ['$event'])
    handleKeyboardEvent(event: KeyboardEvent) {
        if (event.key === 'ArrowRight' || event.key === 'ArrowUp' || event.key === 'Enter') {
            this.moveNext();
        } else if (event.key === 'Backspace' || event.key === 'ArrowLeft' || event.key === 'ArrowDown') {
            this.movePrev();
        } else if (event.key === ' ') {
            this.maximize();
        } else if (event.key === 'Escape') {
            this.minimize();
        }
    }

    get currentIndex(): number {
        return this.iterator.currentIndex;
    }

    get base64Image(): string | undefined {
        return this.story?.ImagePreviewUrl;
    }

    get contents(): string[] {
        return this.story?.Contents?.map((contentModel) => contentModel.Content) ?? [];
    }

    get currentPageContent(): string {
        return this.contents.at(this.currentIndex) ?? 'Page is empty';
    }

    get currentPageLabel(): string {
        return `Page: ${this.currentIndex + 1} / ${this.contents.length}`;
    }

    moveNext(): boolean {
        const result = this.iterator.moveNext();

        if (result && this.storyId) {
            this.storyService
                .read({
                    StoryId: this.storyId,
                    PageRead: this.currentIndex,
                })
                .subscribe();
        }

        return result;
    }

    movePrev(): boolean {
        return this.iterator.movePrev();
    }

    maximize(): void {
        this.maximized = true;
    }

    minimize(): void {
        this.maximized = false;
    }
}
