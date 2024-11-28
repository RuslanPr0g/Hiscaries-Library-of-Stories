/* eslint-disable @typescript-eslint/no-explicit-any */
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationStateService } from '../../services/statefull/notification-state.service';
import { NotificationModel } from '../../models/notification.model';
import { DestroyService } from '../../services/destroy.service';
import { takeUntil } from 'rxjs';

@Component({
    selector: 'app-notifications-bar',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './notifications-bar.component.html',
    styleUrls: ['./notifications-bar.component.scss'],
    providers: [DestroyService],
})
export class NotificationsBarComponent implements OnInit {
    notifications: NotificationModel[] = [];

    constructor(
        private notificationStateService: NotificationStateService<any>,
        private destroyService: DestroyService
    ) {}

    ngOnInit(): void {
        this.notificationStateService.notifications$
            .pipe(takeUntil(this.destroyService.subject$))
            .subscribe((notifications) => {
                this.notifications = notifications;
                this.notifications = [
                    {
                        Id: '1',
                        UserId: '1',
                        Type: 'DD',
                        Message: 'Ffrdsafgsr gsedrg serg seg srdgserfg vsdrgh drg esgvdfg sedrgsegsdfgsdf',
                        IsRead: false,
                    },
                    {
                        Id: '2',
                        UserId: '1',
                        Type: 'DD 2',
                        Message: 'message 2',
                        IsRead: true,
                    },
                ];
            });
    }

    markAllAsRead(): void {
        const unReadNotifications = this.notifications.filter((x) => !x.IsRead);
        this.notificationStateService.markNotificationsAsRead(unReadNotifications);
    }
}
