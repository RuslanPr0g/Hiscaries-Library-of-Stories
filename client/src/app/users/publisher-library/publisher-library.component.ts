import { Component, OnInit } from '@angular/core';
import { LibraryComponent } from '../library/library.component';
import { ActivatedRoute, Router } from '@angular/router';
import { LibraryModel } from '../models/domain/library.model';
import { UserService } from '../services/user.service';
import { take } from 'rxjs';
import { NavigationConst } from '../../shared/constants/navigation.const';
import { StoryService } from '../../stories/services/story.service';
import { StoryModel } from '../../stories/models/domain/story-model';
import { UserNotificationService } from '../services/notification.service';

@Component({
    selector: 'app-publisher-library',
    standalone: true,
    imports: [LibraryComponent],
    templateUrl: './publisher-library.component.html',
    styleUrl: './publisher-library.component.scss',
})
export class PublisherLibraryComponent implements OnInit {
    libraryInfo: LibraryModel;
    libraryId: string | null;
    stories: StoryModel[];

    isLoading: boolean = false;
    isSubscribeLoading: boolean = false;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private userService: UserService,
        private storyService: StoryService,
        private notificationService: UserNotificationService
    ) {
        this.libraryId = this.route.snapshot.paramMap.get('id');
    }

    ngOnInit(): void {
        if (!this.libraryId) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }

        this.isLoading = true;

        this.fetchLibrary(this.libraryId);

        this.storyService
            .getStoriesByLibraryId(this.libraryId)
            .pipe(take(1))
            .subscribe((stories) => {
                this.stories = stories;
                this.isLoading = false;
            });
    }

    get isSubscribed(): boolean {
        return this.libraryInfo?.IsSubscribed ?? false;
    }

    subscribeAction(): void {
        this.subscribeToLibrary(this.libraryId!);
    }

    unSubscribeAction(): void {
        this.unSubscribeFromLibrary(this.libraryId!);
    }

    private subscribeToLibrary(libraryId: string): void {
        this.isSubscribeLoading = true;

        this.userService
            .subscribeToLibrary({ LibraryId: libraryId })
            .pipe(take(1))
            .subscribe({
                next: () => {
                    this.fetchLibrary(this.libraryId!);
                    this.finishSubscribeLoading();
                },
                error: () => {
                    this.finishSubscribeLoading();
                },
            });
    }

    private unSubscribeFromLibrary(libraryId: string): void {
        this.isSubscribeLoading = true;

        this.userService
            .unsubscribeFromLibrary({ LibraryId: libraryId })
            .pipe(take(1))
            .subscribe({
                next: () => {
                    this.fetchLibrary(this.libraryId!);
                    this.finishSubscribeLoading();
                },
                error: () => {
                    this.finishSubscribeLoading();
                },
            });
    }

    private finishSubscribeLoading(): void {
        setTimeout(() => {
            this.isSubscribeLoading = false;
        }, 1000);
    }

    private fetchLibrary(libraryId: string): void {
        this.userService
            .getLibrary(libraryId)
            .pipe(take(1))
            .subscribe({
                next: (library) => this.processLibraryFetch(library),
                error: () => {
                    this.router.navigate([NavigationConst.Home]);
                },
            });
    }

    private processLibraryFetch(library: LibraryModel): void {
        this.isLoading = false;

        if (!library) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }

        if (library.IsLibraryOwner) {
            this.router.navigate([NavigationConst.MyLibrary]);
            return;
        }

        this.libraryInfo = library;
    }
}
