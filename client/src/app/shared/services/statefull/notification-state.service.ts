import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class NotificationStateService<T> {
    private unreadNotificationsCount = new BehaviorSubject<number>(0);
    private notifications = new BehaviorSubject<T[]>([]);

    unreadCount$ = this.unreadNotificationsCount.asObservable();
    notifications$ = this.notifications.asObservable();

    addNotification(notification: T): void {
        const currentNotifications = this.notifications.value;
        this.notifications.next([notification, ...currentNotifications]);

        const currentUnreadCount = this.unreadNotificationsCount.value;
        this.unreadNotificationsCount.next(currentUnreadCount + 1);
    }

    markAllAsRead(): void {
        this.unreadNotificationsCount.next(0);
    }
}
