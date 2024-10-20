import { Component } from '@angular/core';
import { StoryService } from '../../stories/services/story.service';
import { take } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  stories: any[] = [];
  
  constructor(
    private storyService: StoryService
  ) {
    this.storyService.recommendations()
    .pipe(take(1))
    .subscribe(stories => {
      console.warn(stories);
      this.stories = stories.map(s => ({
        ...s,
        ImagePreview: this.convertToBase64(s.ImagePreview)
      }));
    });
  }

  convertToBase64(byteArray: any): string {
      return 'data:image/jpeg;base64,' + byteArray;
  }
}
