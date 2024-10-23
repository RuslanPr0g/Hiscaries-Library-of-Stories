import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { StoryModel } from '../models/domain/story-model';
import { Router } from '@angular/router';
import { NavigationConst } from '../../shared/constants/navigation.const';

@Component({
  selector: 'app-story-search-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './story-search-item.component.html',
  styleUrl: './story-search-item.component.scss'
})
export class SearchStoryItemComponent {
  @Input() story: StoryModel;

  constructor(
    private router: Router
  ) {
  }

  previewStory(story: StoryModel): void {
    this.router.navigate([NavigationConst.PreviewStory(story.Id)]);
  }
}
