import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoryModel } from '../models/domain/story-model';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-preview-story',
  standalone: true,
  imports: [],
  templateUrl: './preview-story.component.html',
  styleUrl: './preview-story.component.scss'
})
export class PreviewStoryComponent implements OnInit {
  private storyId: string | null = null;

  story: StoryModel | null = null;
  storyNotFound: boolean = false;

  constructor(
    private route: ActivatedRoute,
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
          this.story = story;
        }

        console.warn(stories, story);
      });
  }
}
