import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root',
})
export class UserNotificationService {
    private hubConnection: signalR.HubConnection;
    constructor(private authService: AuthService) {
        const token = this.authService.getToken();

        if (token) {
            this.hubConnection = new signalR.HubConnectionBuilder()
                .configureLogging(signalR.LogLevel.Trace)
                .withUrl('/hubs/usernotifications', {
                    accessTokenFactory: () => token,
                    transport: signalR.HttpTransportType.ServerSentEvents,
                })
                .withAutomaticReconnect()
                .build();

            this.hubConnection.on('StoryPublished', (message) => {
                console.warn(`||||||||||StoryPublished_____ Message of the notification: ${message}`);
            });

            this.hubConnection.onclose((error) => {
                console.log(error);
            });
            this.hubConnection.onreconnecting((error) => {
                console.warn('Reconnecting...', error);
            });
            this.hubConnection.onreconnected((connectionId) => {
                console.log('Reconnected:', connectionId);
            });

            this.hubConnection.start().catch((err) => console.error(err));
        }
    }
}
