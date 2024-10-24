import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoryModelWithContents } from '../models/domain/story-model';
import { CommonModule } from '@angular/common';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { convertToBase64 } from '../../shared/helpers/image.helper';
import { IteratorService } from '../../shared/services/iterator.service';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-read-story-content',
  standalone: true,
  imports: [CommonModule, ButtonModule],
  providers: [IteratorService],
  templateUrl: './read-story-content.component.html',
  styleUrl: './read-story-content.component.scss'
})
export class ReadStoryContentComponent implements OnInit {
  storyId: string | null = null;

  globalError: string | null = null;
  story: StoryModelWithContents | null = null;
  storyNotFound: boolean = false;

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

    this.storyService.getStoryByIdWithContents({
      Id: this.storyId
    })
      .pipe(take(1))
      .subscribe({
        next: story => {
          if (!story) {
            this.storyNotFound = true;
            return;
          }

          this.story = {
            ...story,
            ImagePreview: convertToBase64(story.ImagePreview)
          };

          this.iterator.upperBoundary = story.Contents.length - 1;
        },
        error: () => this.storyNotFound = true
      });
  }

  @HostListener('document:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key === 'ArrowRight' || event.key === ' ' || event.key === 'Enter') {
      this.moveNext();
    } else if (event.key === 'Backspace' || event.key === 'ArrowLeft') {
      this.movePrev();
    }
  }

  get currentIndex(): number {
    return this.iterator.currentIndex;
  }

  get base64Image(): any {
    return this.story?.ImagePreview;
  }

  get contents(): string[] {
    return this.story?.Contents?.map(contentModel => contentModel.Content) ?? [];
  }

  get currentPageContent(): string {
    return this.contents.at(this.currentIndex) ?? 'Page is empty';
  }

  get currentPageLabel(): string {
    return `Page: ${(this.currentIndex + 1)} / ${this.contents.length}`;
  }

  moveNext(): boolean {
    return this.iterator.moveNext();
  }

  movePrev(): boolean {
    return this.iterator.movePrev();
  }
}
