import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LibraryModel } from '../models/domain/library.model';
import { EditLibraryRequest } from '../models/requests/edit-library.model';
import { LibrarySubscriptionRequest } from '../models/requests/library-subscription.model';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private apiUrl: string = `${environment.apiUrl}/users`;

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
