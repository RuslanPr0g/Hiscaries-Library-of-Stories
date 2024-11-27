import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchStoryRecommendationsComponent } from '../../stories/search-story-recommendations/search-story-recommendations.component';
import { SearchStoryResumeReadingComponent } from '../../stories/search-story-resume-reading/search-story-resume-reading.component';
import { UserNotificationService } from '../services/notification.service';

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [CommonModule, SearchStoryRecommendationsComponent, SearchStoryResumeReadingComponent],
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
    // TODO: create wrapper after app component to handle notifications globally across the application
    // or somehow use it in the app component
    constructor(private notificationService: UserNotificationService) {}
}
