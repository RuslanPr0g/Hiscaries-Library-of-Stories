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
    private _socialNetworks: { [key: string]: string } = {
        tiktok: 'pi-tiktok',
        youtube: 'pi-youtube',
        facebook: 'pi-facebook',
        instagram: 'pi-instagram',
        twitter: 'pi-twitter',
        linkedin: 'pi-linkedin',
        pinterest: 'pi-pinterest',
    };

    @Input() library: LibraryModel;
    @Input() stories: StoryModel[];
    @Input() isLoading: boolean = false;

    getSocialNetworkIcon(link: string): string {
        if (!link) {
            return '';
        }

        const hostname = new URL(link).hostname.toLowerCase();

        for (const key in this._socialNetworks) {
            if (hostname.includes(key)) {
                return this._socialNetworks[key];
            }
        }

        return 'pi-link';
    }
}
