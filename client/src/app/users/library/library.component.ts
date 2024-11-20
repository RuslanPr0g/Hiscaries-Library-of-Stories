import { Component, Input } from '@angular/core';
import { LibraryModel } from '../models/domain/Library.model';
import { StoryModel } from '../../stories/models/domain/story-model';
import { CommonModule } from '@angular/common';
import { SearchStoryResultsComponent } from '../../stories/search-story-results/search-story-results.component';

@Component({
    selector: 'app-library',
    standalone: true,
    imports: [CommonModule, SearchStoryResultsComponent],
    templateUrl: './library.component.html',
    styleUrl: './library.component.scss',
})
export class LibraryComponent {
    @Input() library: LibraryModel;
    @Input() stories: StoryModel[];
    @Input() isLoading: boolean = false;
}
