import { Component, OnInit } from '@angular/core';
import { LibraryComponent } from '../library/library.component';
import { ActivatedRoute, Router } from '@angular/router';
import { LibraryModel } from '../models/domain/Library.model';
import { UserService } from '../services/user.service';
import { take } from 'rxjs';
import { NavigationConst } from '../../shared/constants/navigation.const';

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

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private userService: UserService
    ) {
        this.libraryId = this.route.snapshot.paramMap.get('id');
    }

    ngOnInit(): void {
        if (!this.libraryId) {
            this.router.navigate([NavigationConst.Home]);
            return;
        }

        this.userService
            .getLibrary(this.libraryId)
            .pipe(take(1))
            .subscribe((library) => {
                if (!library) {
                    // todo: show error
                    return;
                }

                if (library.IsLibraryOwner) {
                    this.router.navigate([NavigationConst.MyLibrary]);
                    return;
                }

                this.libraryInfo = library;

                // TODO: add error in case if library is not found in the error => lambda
            });
    }
}
