import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoryModelWithContents } from '../models/domain/story-model';
import { CommonModule } from '@angular/common';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { convertToBase64 } from '../../shared/helpers/image.helper';

@Component({
  selector: 'app-read-story-content',
  standalone: true,
  imports: [CommonModule],
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
        },
        error: () => this.storyNotFound = true
      });
  }

  get base64Image(): any {
    return this.story?.ImagePreview;
  }

  get contents(): string[] {
    return this.story?.Contents?.map(contentModel => contentModel.Content) ?? [];
  }
}
