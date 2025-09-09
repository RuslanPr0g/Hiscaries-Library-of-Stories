import { Component, inject } from '@angular/core';
import { LibraryComponent } from '@users/library/library.component';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap, finalize, take, tap } from 'rxjs';
import { NavigationConst } from '@shared/constants/navigation.const';
import { UserService } from '@users/services/user.service';
import { StoryWithMetadataService } from '@user-to-story/services/multiple-services-merged/story-with-metadata.service';
import { PaginationService } from '@shared/services/statefull/pagination.service';
import { LibraryModel } from '@users/models/domain/library.model';
import { CommonModule } from '@angular/common';
import { emptyQueriedResult } from '@shared/models/queried.model';

@Component({
    selector: 'app-publisher-library',
    standalone: true,
    imports: [LibraryComponent, CommonModule],
    templateUrl: './publisher-library.component.html',
    styleUrls: ['./publisher-library.component.scss'],
    providers: [PaginationService],
})
export class PublisherLibraryComponent {
    private router = inject(Router);
    private route = inject(ActivatedRoute);
    private userService = inject(UserService);
    private storyService = inject(StoryWithMetadataService);
    pagination = inject(PaginationService);
    empty = emptyQueriedResult;

    libraryId = this.route.snapshot.paramMap.get('id');
    libraryInfo: LibraryModel | null = null;

    isSubscribeLoading = false;

    stories$ = this.pagination.query$.pipe(
        // tap(() => this.pagination.setLoading(true)),
        switchMap((q) => {
            return this.storyService
                .getStoriesByLibrary({
                    LibraryId: this.libraryId ?? '',
                    QueryableModel: q,
                })
                .pipe(finalize(() => this.pagination.setLoading(false)));
        })
    );

    isLoading$ = this.pagination.isLoading$;

    constructor() {
        if (!this.libraryId) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }

        this.fetchLibrary(this.libraryId);
    }

    get isSubscribed(): boolean {
        return this.libraryInfo?.IsSubscribed ?? false;
    }

    subscribeAction(): void {
        this.updateSubscription(true);
    }

    unSubscribeAction(): void {
        this.updateSubscription(false);
    }

    private updateSubscription(subscribe: boolean): void {
        if (!this.libraryId) return;

        this.isSubscribeLoading = true;
        const action = subscribe
            ? this.userService.subscribeToLibrary({ LibraryId: this.libraryId })
            : this.userService.unsubscribeFromLibrary({ LibraryId: this.libraryId });

        action.pipe(take(1)).subscribe({
            next: () => {
                const newCount = (this.libraryInfo?.SubscribersCount ?? 0) + (subscribe ? 1 : -1);
                this.fetchLibrary(this.libraryId!);
                this.finishSubscribeLoading(newCount);
            },
            error: () => {
                this.finishSubscribeLoading();
            },
        });
    }

    private finishSubscribeLoading(subCount: number = -1): void {
        setTimeout(() => {
            if (subCount >= 0 && this.libraryInfo) {
                this.libraryInfo.SubscribersCount = subCount;
            }
            this.isSubscribeLoading = false;
        }, 1000);
    }

    private fetchLibrary(libraryId: string): void {
        this.pagination.setLoading(true);

        this.userService
            .getLibrary(libraryId)
            .pipe(take(1))
            .subscribe({
                next: (library) => this.processLibraryFetch(library),
                error: () => this.router.navigate([NavigationConst.Home]),
            });
    }

    private processLibraryFetch(library: LibraryModel): void {
        this.pagination.setLoading(false);

        if (!library) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }

        if (library.IsLibraryOwner) {
            this.router.navigate([NavigationConst.MyLibrary]);
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
