import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
    providedIn: 'root',
})
export class UserNotificationService {
    private hubConnection: signalR.HubConnection;
    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Trace)
            .withUrl('/hubs/usernotifications')
            .build();
        this.hubConnection.on('ReceiveNotification', (user) => {
            console.log(`User: ${user}`);
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

    sendMessage(user: string, message: string): void {
        this.hubConnection.invoke('SendMessage', user, message).catch((err) => console.error(err));
    }
}
