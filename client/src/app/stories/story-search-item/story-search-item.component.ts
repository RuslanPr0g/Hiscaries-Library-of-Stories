import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { StoryModel } from '../models/domain/story-model';

@Component({
  selector: 'app-story-search-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './story-search-item.component.html',
  styleUrl: './story-search-item.component.scss'
})
export class SearchStoryItemComponent {
  @Input() story: StoryModel;
}
