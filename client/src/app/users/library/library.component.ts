import { Component, Input } from '@angular/core';
import { LibraryModel } from '../models/domain/library.model';
import { StoryModel } from '../../stories/models/domain/story-model';
import { CommonModule } from '@angular/common';
import { SearchStoryResultsComponent } from '../../stories/search-story-results/search-story-results.component';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { LibraryGeneralViewComponent } from './library-general-view/library-general-view.component';
import { LibraryGeneralEditComponent } from './library-general-edit/library-general-edit.component';

@Component({
    selector: 'app-library',
    standalone: true,
    imports: [
        CommonModule,
        ProgressSpinnerModule,
        SearchStoryResultsComponent,
        LibraryGeneralViewComponent,
        LibraryGeneralEditComponent,
    ],
    templateUrl: './library.component.html',
    styleUrl: './library.component.scss',
})
export class LibraryComponent {
    @Input() library: LibraryModel;
    @Input() stories: StoryModel[];
    @Input() isLoading: boolean = false;
    @Input() isAbleToEdit: boolean = false;

    isEditMode: boolean = false;

    startEdit(): void {
        this.isEditMode = true;
    }

    cancelEdit(): void {
        this.isEditMode = false;
    }

    saveEdit(model: LibraryModel): void {
        console.warn(model);
        this.isEditMode = false;
    }
}
