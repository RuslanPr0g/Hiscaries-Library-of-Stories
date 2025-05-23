import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EditLibraryRequest } from '../models/requests/edit-library.model';
import { LibrarySubscriptionRequest } from '../models/requests/library-subscription.model';
import { LibraryModel } from '../models/domain/library.model';
import { ReadStoryRequest } from '../models/requests/read-story.model';
import { UserReadingStoryRequest } from '../models/requests/user-reading-story.model';
import { UserReadingStoryMetadataResponse } from '../models/response/user-reading-story-metadata.model';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private apiUrl = `${environment.apiUrl}/users`;

    constructor(private http: HttpClient) {}

    getUserReadingStoryMetadata(request: UserReadingStoryRequest): Observable<UserReadingStoryMetadataResponse[]> {
        return this.http.post<UserReadingStoryMetadataResponse[]>(this.apiUrl + '/story-metadata', request);
    }

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

    myLibraries(): Observable<string[]> {
        return this.http.get<string[]>(this.apiUrl + '/my-libraries');
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

    read(request: ReadStoryRequest): Observable<void> {
        return this.http.post<void>(`${this.apiUrl}/read`, request);
    }

    resumeReading(): Observable<string[]> {
        return this.http.get<string[]>(`${this.apiUrl}/resume-reading`);
    }

    readingHistory(): Observable<string[]> {
        return this.http.get<string[]>(`${this.apiUrl}/reading-history`);
    }
}
