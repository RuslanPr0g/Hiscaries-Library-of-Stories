import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LibraryModel } from '../../models/domain/Library.model';
import { SocialMediaIconMapperService } from '../../../shared/services/social-media-icon-mapper.service';

@Component({
    selector: 'app-library-general-edit',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './library-general-edit.component.html',
    styleUrl: './library-general-edit.component.scss',
})
export class LibraryGeneralEditComponent {
    @Input() library: LibraryModel;
    @Input() isAbleToEdit: boolean = false;

    @Output() editCancelled = new EventEmitter<void>();
    @Output() editSaved = new EventEmitter<LibraryModel>();

    constructor(private iconService: SocialMediaIconMapperService) {}

    getSocialNetworkIcon(link: string): string {
        return this.iconService.mapFromUrl(link);
    }

    cancelEdit(): void {
        this.editCancelled?.emit();
    }

    saveEdit(): void {
        // TODO: provide this.form.value
        this.editSaved?.emit();
    }
}
