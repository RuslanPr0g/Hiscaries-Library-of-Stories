import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StoryModel } from '../models/domain/story-model';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { convertToBase64 } from '../../shared/helpers/image.helper';
import { FormButtonComponent } from '../../shared/components/form-button/form-button.component';

@Component({
  selector: 'app-preview-story',
  standalone: true,
  imports: [CommonModule, FormButtonComponent],
  templateUrl: './preview-story.component.html',
  styleUrl: './preview-story.component.scss'
})
export class PreviewStoryComponent implements OnInit {
  private storyId: string | null = null;

  story: StoryModel | null = null;
  storyNotFound: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private storyService: StoryService
  ) { }

  ngOnInit(): void {
    this.storyId = this.route.snapshot.paramMap.get('id');

    this.storyService.searchStory({
      Id: this.storyId
    })
      .pipe(take(1))
      .subscribe(stories => {
        const story = stories[0];
        if (!story) {
          this.storyNotFound = true;
        } else {
          this.story = {
            ...story,
            ImagePreview: convertToBase64(story.ImagePreview)
          };
        }
      });
  }

  get isEditable(): boolean {
    return this.story?.IsEditable ?? false;
  }

  readStory(): void {
    console.log('User wants to read story...');
  }

  modifyStory(): void {
    this.router.navigateByUrl(`/edit/${this.storyId}`);
  }
}
