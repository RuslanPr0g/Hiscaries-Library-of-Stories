import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
    providedIn: 'root',
})
export class UserNotificationService {
    private hubConnection: signalR.HubConnection;
    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder().withUrl('/notificationhub').build();
        this.hubConnection.on('ReceiveNotification', (user, message) => {
            console.log(`User: ${user}, Message: ${message}`);
        });
        this.hubConnection.start().catch((err) => console.error(err));
    }

    sendMessage(user: string, message: string): void {
        this.hubConnection.invoke('SendMessage', user, message).catch((err) => console.error(err));
    }
}
