import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './shared/header/header.component';
import { LoadingOverlayComponent } from './shared/components/loading-overlay/loading-overlay.component';
import { CommonModule } from '@angular/common';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { NavigationConst } from './shared/constants/navigation.const';
import { AuthService } from './users/services/auth.service';
import { SearchBarComponent } from './shared/components/search-bar/search-bar.component';
import { NotificationLifecycleManagerService } from './users/services/notification-lifecycle-manager.service';
import { StoryPublishedHandler } from './users/notification-handlers/story-published-notification.handler';

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
        SearchBarComponent,
    ],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent {
    title = 'hiscaries';

    loading: boolean = true;
    sidebarVisible: boolean = false;

    constructor(
        private router: Router,
        public authService: AuthService,
        private notificationManagerService: NotificationLifecycleManagerService
        // TODO: make it work
        // @Inject(NOTIFICATION_HANDLERS) private notificationHandlers: NotificationHandler[]
    ) {}

    ngOnInit() {
        setTimeout(() => {
            this.fadeOutLoading();

            if (this.authService.isAuthenticated()) {
                // TODO: fix DI
                this.notificationManagerService.initialize([new StoryPublishedHandler()]);
            }
        }, 1501);

        this.authService.loginEvent$.subscribe(() => {
            // TODO: fix DI
            this.notificationManagerService.initialize([new StoryPublishedHandler()]);
        });

        this.authService.logoutEvent$.subscribe(() => {
            this.notificationManagerService.stop();
        });
    }

    fadeOutLoading() {
        this.loading = false;
    }

    home(): void {
        this.router.navigate([NavigationConst.Home]);
    }
}
