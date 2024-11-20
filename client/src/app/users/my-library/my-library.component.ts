import { Component, OnInit } from '@angular/core';
import { LibraryComponent } from '../library/library.component';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NavigationConst } from '../../shared/constants/navigation.const';
import { UserService } from '../services/user.service';
import { LibraryModel } from '../models/domain/Library.model';
import { take } from 'rxjs';

@Component({
    selector: 'app-my-library',
    standalone: true,
    imports: [LibraryComponent],
    templateUrl: './my-library.component.html',
    styleUrl: './my-library.component.scss',
})
export class MyLibraryComponent implements OnInit {
    libraryInfo: LibraryModel;

    constructor(
        private router: Router,
        private authService: AuthService,
        private userService: UserService
    ) {
        if (!this.authService.isPublisher()) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }
    }

    ngOnInit(): void {
        this.userService
            .getLibrary()
            .pipe(take(1))
            .subscribe((library) => {
                if (!library) {
                    // todo: show error
                    return;
                }

                this.libraryInfo = library;
            });
    }
}
