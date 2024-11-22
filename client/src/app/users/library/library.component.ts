import { Component, EventEmitter, Input, Output } from '@angular/core';
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

    @Input() isAbleToSubscribe: boolean = false;
    @Input() isSubscribed: boolean = false;

    @Input() isSubscribeLoading: boolean = false;

    @Output() libraryEdited = new EventEmitter<LibraryModel>();

    @Output() subscribed = new EventEmitter<void>();
    @Output() unSubscribed = new EventEmitter<void>();

    isEditMode: boolean = false;

    startEdit(): void {
        this.isEditMode = true;
    }

    cancelEdit(): void {
        this.isEditMode = false;
    }

    saveEdit(model: LibraryModel): void {
        this.libraryEdited?.emit(model);
        this.isEditMode = false;
    }

    subscribeAction(): void {
        this.subscribed?.emit();
    }

    unSubscribeAction(): void {
        this.unSubscribed?.emit();
    }
}
