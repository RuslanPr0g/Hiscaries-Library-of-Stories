import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { StoryModel } from '../models/domain/story-model';
import { SearchStoryItemComponent } from '../story-search-item/story-search-item.component';
import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-search-story-results',
  standalone: true,
  imports: [CommonModule, SearchStoryItemComponent, SkeletonModule],
  templateUrl: './search-story-results.component.html',
  styleUrl: './search-story-results.component.scss'
})
export class SearchStoryResultsComponent {
  stories: StoryModel[] = [];

  @Input() initialStories: StoryModel[];
  
  constructor(
    private storyService: StoryService
  ) {
    if (!this.initialStories || this.initialStories.length === 0) {
      this.storyService.recommendations()
      .pipe(take(1))
      .subscribe(stories => {
        this.stories = stories.map(s => ({
          ...s,
          ImagePreview: this.convertToBase64(s.ImagePreview)
        }));
      });
    }
  }

  private convertToBase64(byteArray: any): string {
      return 'data:image/jpeg;base64,' + byteArray;
  }
}
