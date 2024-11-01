import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './shared/header/header.component';
import { LoadingOverlayComponent } from './shared/components/loading-overlay/loading-overlay.component';
import { CommonModule } from '@angular/common';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { NavigationConst } from './shared/constants/navigation.const';
import { UserService } from './users/services/user.service';
import { SearchInputComponent } from './shared/components/search-input/search-input.component';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [
        CommonModule,
        RouterOutlet,
        ButtonModule,
        SidebarModule,
        HeaderComponent,
        LoadingOverlayComponent,
        SearchInputComponent,
    ],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent {
    title = 'hiscaries';

    searchTerm?: string | null;
    loading: boolean = true;
    sidebarVisible: boolean = false;

    constructor(
        private router: Router,
        public userService: UserService
    ) {}

    ngOnInit() {
        setTimeout(() => {
            this.fadeOutLoading();
        }, 1501);
    }

    fadeOutLoading() {
        this.loading = false;
    }

    home(): void {
        this.router.navigate([NavigationConst.Home]);
    }

    search(term: string): void {
        this.router.navigate([NavigationConst.SearchStory(term)]);
    }
}
