import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchStoryRecommendationsComponent } from '../../stories/search-story-recommendations/search-story-recommendations.component';

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [CommonModule, SearchStoryRecommendationsComponent],
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})
export class HomeComponent {}
