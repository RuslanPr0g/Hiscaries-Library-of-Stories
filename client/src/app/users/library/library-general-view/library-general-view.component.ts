import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LibraryModel } from '../../models/domain/library.model';
import { SocialMediaIconMapperService } from '../../../shared/services/social-media-icon-mapper.service';

@Component({
    selector: 'app-library-general-view',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './library-general-view.component.html',
    styleUrl: './library-general-view.component.scss',
})
export class LibraryGeneralViewComponent {
    @Input() library: LibraryModel;
    @Input() isAbleToEdit: boolean = false;

    @Output() editStarted = new EventEmitter<void>();

    constructor(private iconService: SocialMediaIconMapperService) {}

    getSocialNetworkIcon(link: string): string {
        return this.iconService.mapFromUrl(link);
    }

    startEdit(): void {
        this.editStarted?.emit();
    }
}
