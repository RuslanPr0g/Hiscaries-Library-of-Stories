import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EditLibraryRequest } from '../models/requests/edit-library.model';
import { LibrarySubscriptionRequest } from '../models/requests/library-subscription.model';
import { NotificationModel } from '../../shared/models/notification.model';
import { LibraryModel } from '../models/domain/library.model';
import { ReadNotificationsRequest } from '../models/requests/read-notifications.model';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private apiUrl = `${environment.apiUrl}/users`;

    constructor(private http: HttpClient) {}

    becomePublisher(): Observable<void> {
        return this.http.post<void>(this.apiUrl + '/become-publisher', {});
    }

    getLibrary(libraryId?: string): Observable<LibraryModel> {
        if (libraryId) {
            const params = new HttpParams().set('libraryId', libraryId);
            return this.http.get<LibraryModel>(this.apiUrl + '/libraries', { params: params });
        }

        return this.http.get<LibraryModel>(this.apiUrl + '/libraries');
    }

    notifications(): Observable<NotificationModel[]> {
        return this.http.get<NotificationModel[]>(this.apiUrl + '/notifications');
    }

    readNotifications(request: ReadNotificationsRequest): Observable<void> {
        return this.http.post<void>(this.apiUrl + '/notifications', request);
    }

    editLibrary(request: EditLibraryRequest): Observable<void> {
        return this.http.put<void>(this.apiUrl + '/libraries', request);
    }

    subscribeToLibrary(request: LibrarySubscriptionRequest): Observable<void> {
        return this.http.post<void>(this.apiUrl + '/libraries/subscriptions/subscribe', request);
    }

    unsubscribeFromLibrary(request: LibrarySubscriptionRequest): Observable<void> {
        return this.http.post<void>(this.apiUrl + '/libraries/subscriptions/unsubscribe', request);
    }
}
