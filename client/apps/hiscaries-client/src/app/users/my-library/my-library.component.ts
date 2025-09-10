import { Component, inject, signal } from '@angular/core';
import { LibraryComponent } from '@users/library/library.component';
import { Router } from '@angular/router';
import { take } from 'rxjs';
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
    isLoading = signal(false);

    constructor() {
        if (!this.authService.isPublisher()) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }
        this.fetchLibrary();
    }

    private fetchLibrary() {
        this.isLoading.set(true);
        this.userService
            .getLibrary()
            .pipe(take(1))
            .subscribe({
                next: (library) => this.processLibrary(library),
                error: () => this.router.navigate([NavigationConst.Home]),
            });
    }

    private processLibrary(library: LibraryModel) {
        if (!library) return this.router.navigate([NavigationConst.Home]);
        if (!library.IsLibraryOwner || !this.authService.isTokenOwnerByUsername(library.PlatformUser.Username)) {
            return this.router.navigate([NavigationConst.PublisherLibrary(library.Id)]);
        }
        this.libraryInfo = library;
        this.pagination.reset();
        this.loadStories();
        return null;
    }

    private loadStories() {
        if (!this.libraryInfo) return;
        this.isLoading.set(true);
        this.storyService
            .getStoriesByLibrary({ LibraryId: this.libraryInfo.Id, QueryableModel: this.pagination.snapshot })
            .pipe(take(1))
            .subscribe({
                next: (data) => this.stories.set(data),
                complete: () => this.isLoading.set(false),
                error: () => this.isLoading.set(false),
            });
    }

    editLibrary(model: LibraryModel) {
        const shouldUpdateAvatar = !model.AvatarUrl || model.AvatarUrl.startsWith('data');
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

    nextPage() {
        this.pagination.nextPage();
        this.loadStories();
    }

    prevPage() {
        this.pagination.prevPage();
        this.loadStories();
    }
}
