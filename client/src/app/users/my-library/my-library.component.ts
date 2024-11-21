import { Component, OnInit } from '@angular/core';
import { LibraryComponent } from '../library/library.component';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NavigationConst } from '../../shared/constants/navigation.const';
import { UserService } from '../services/user.service';
import { LibraryModel } from '../models/domain/Library.model';
import { take } from 'rxjs';
import { StoryModel } from '../../stories/models/domain/story-model';
import { StoryService } from '../../stories/services/story.service';

@Component({
    selector: 'app-my-library',
    standalone: true,
    imports: [LibraryComponent],
    templateUrl: './my-library.component.html',
    styleUrl: './my-library.component.scss',
})
export class MyLibraryComponent implements OnInit {
    libraryInfo: LibraryModel;
    stories: StoryModel[];
    isLoading: boolean = false;

    constructor(
        private router: Router,
        private authService: AuthService,
        private userService: UserService,
        private storyService: StoryService
    ) {
        if (!this.authService.isPublisher()) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }
    }

    ngOnInit(): void {
        this.isLoading = true;

        this.userService
            .getLibrary()
            .pipe(take(1))
            .subscribe({
                next: (library: LibraryModel) => {
                    if (!library) {
                        this.router.navigate([NavigationConst.Home]);
                        return;
                    }

                    if (
                        !library.IsLibraryOwner ||
                        !this.authService.isTokenOwnerByUsername(library.PlatformUser.Username)
                    ) {
                        this.router.navigate([NavigationConst.PublisherLibrary(library.Id)]);
                    }

                    this.libraryInfo = library;

                    this.storyService
                        .getStoriesByLibraryId(this.libraryInfo.Id)
                        .pipe(take(1))
                        .subscribe((stories) => {
                            this.stories = stories;
                            this.isLoading = false;
                        });
                },
                error: () => {
                    this.router.navigate([NavigationConst.Home]);
                },
            });
    }
}
