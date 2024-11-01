import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchStoryResultsComponent } from '../../stories/search-story-results/search-story-results.component';

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})
export class HomeComponent {}
