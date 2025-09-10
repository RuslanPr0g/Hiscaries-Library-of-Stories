import { Component, inject, effect, signal } from '@angular/core';
import { LibraryComponent } from '@users/library/library.component';
import { Router } from '@angular/router';
import { finalize, take } from 'rxjs';
import { AuthService } from '@users/services/auth.service';
import { UserService } from '@users/services/user.service';
import { StoryWithMetadataService } from '@user-to-story/services/multiple-services-merged/story-with-metadata.service';
import { PaginationService } from '@shared/services/statefull/pagination.service';
import { NavigationConst } from '@shared/constants/navigation.const';
import { LibraryModel } from '@users/models/domain/library.model';
import { CommonModule } from '@angular/common';
import { emptyQueriedResult, QueriedModel } from '@shared/models/queried.model';
import { StoryModel } from '@stories/models/domain/story-model';

@Component({
    selector: 'app-my-library',
    standalone: true,
    imports: [LibraryComponent, CommonModule],
    templateUrl: './my-library.component.html',
    styleUrls: ['./my-library.component.scss'],
    providers: [PaginationService],
})
export class MyLibraryComponent {
    private router = inject(Router);
    private authService = inject(AuthService);
    private userService = inject(UserService);
    private storyService = inject(StoryWithMetadataService);
    pagination = inject(PaginationService);

    libraryInfo: LibraryModel | null = null;

    stories = signal<QueriedModel<StoryModel>>(emptyQueriedResult);
    isLoading = this.pagination.isLoading;

    constructor() {
        if (!this.authService.isPublisher()) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }
        this.fetchLibrary();

        effect(() => {
            if (!this.libraryInfo) return;
            const q = this.pagination.query();
            this.pagination.setLoading(true);
            this.storyService
                .getStoriesByLibrary({
                    LibraryId: this.libraryInfo.Id,
                    QueryableModel: q,
                })
                .pipe(finalize(() => this.pagination.setLoading(false)))
                .subscribe((data) => this.stories.set(data));
        });
    }

    editLibrary(model: LibraryModel) {
        const isBase64 = (v: string) => !v || v.startsWith('data');
        const shouldUpdateAvatar = isBase64(model.AvatarUrl);

        this.userService
            .editLibrary({
                LibraryId: model.Id,
                Bio: model.Bio,
                Avatar: shouldUpdateAvatar ? model.AvatarUrl : null,
                LinksToSocialMedia: model.LinksToSocialMedia,
                ShouldUpdateAvatar: shouldUpdateAvatar,
            })
            .pipe(take(1))
            .subscribe(() => this.fetchLibrary());
    }

    private fetchLibrary(): void {
        this.pagination.setLoading(true);
        this.userService
            .getLibrary()
            .pipe(take(1))
            .subscribe({
                next: (library) => this.processLibraryFetch(library),
                error: () => this.router.navigate([NavigationConst.Home]),
            });
    }

    private processLibraryFetch(library: LibraryModel): void {
        if (!library) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }
        if (!library.IsLibraryOwner || !this.authService.isTokenOwnerByUsername(library.PlatformUser.Username)) {
            this.router.navigate([NavigationConst.PublisherLibrary(library.Id)]);
            return;
        }
        this.libraryInfo = library;
        this.pagination.reset();
    }

    nextPage() {
        this.pagination.nextPage();
    }

    prevPage() {
        this.pagination.prevPage();
    }
}
