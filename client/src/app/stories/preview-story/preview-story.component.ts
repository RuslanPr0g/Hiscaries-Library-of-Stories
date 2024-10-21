import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-preview-story',
  standalone: true,
  imports: [],
  templateUrl: './preview-story.component.html',
  styleUrl: './preview-story.component.scss'
})
export class PreviewStoryComponent implements OnInit {
  storyId: string | null = null;

  constructor(private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    this.storyId = this.route.snapshot.paramMap.get('id');

    console.warn(this.storyId);
  }
}
