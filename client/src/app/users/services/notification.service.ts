import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from './auth.service';
import { SignalRConnectionFactoryService } from '../../shared/services/statefull/signalr-connection-factory.service';
import { NotificationHandler } from '../../shared/models/notification-handler.model';
import { NotificationStateService } from '../../shared/services/statefull/notification-state.service';

class UserNotificationTypes {
    static readonly StoryPublished = 'StoryPublished';
}

@Injectable({
    providedIn: 'root',
})
export class UserNotificationService {
    private notificationHandlers: NotificationHandler[];
    private hubConnection: signalR.HubConnection;
    private readonly hubUrl = '/hubs/usernotifications';

    constructor(
        private authService: AuthService,
        private connectionFactory: SignalRConnectionFactoryService,
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        private notificationStateService: NotificationStateService<any>
    ) {}

    initialize(handlers: NotificationHandler[]): void {
        const token = this.authService.getToken();

        if (!token) {
            console.warn('Token is missing. Cannot establish SignalR connection.');
            return;
        }

        this.hubConnection = this.connectionFactory.createSSEConnection(this.hubUrl, token);

        this.registerEventListeners();

        this.hubConnection
            .start()
            .then(() => console.log('SignalR connection started successfully.'))
            .catch((err) => console.error('Error starting SignalR connection:', err));

        this.notificationHandlers = handlers;
    }

    disconnect(): void {
        if (this.hubConnection) {
            this.hubConnection.stop().then(() => console.log('SignalR connection stopped.'));
        }
    }

    private registerEventListeners(): void {
        this.registerEventHandlers();

        this.hubConnection.onclose((error) => {
            console.error('SignalR connection closed.', error);
        });

        this.hubConnection.onreconnecting((error) => {
            console.warn('SignalR reconnecting...', error);
        });

        this.hubConnection.onreconnected((connectionId) => {
            console.log('SignalR reconnected with connectionId:', connectionId);
        });
    }

    private registerEventHandlers(): void {
        this.hubConnection.on(UserNotificationTypes.StoryPublished, (message) =>
            this.dispatchNotification(UserNotificationTypes.StoryPublished, message)
        );
    }

    private dispatchNotification<T>(eventType: string, payload: T): void {
        this.notificationHandlers.forEach((handler) => handler.handleNotification(eventType, payload));

        this.notificationStateService.addNotification({ eventType, payload });
    }
}
