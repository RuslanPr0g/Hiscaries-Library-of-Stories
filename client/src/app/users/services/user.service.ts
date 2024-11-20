import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Library } from '../models/domain/Library.model';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private apiUrl: string = `${environment.apiUrl}/users`;

    constructor(private http: HttpClient) {}

    becomePublisher(): Observable<void> {
        return this.http.post<void>(this.apiUrl + '/become-publisher', {});
    }

    getLibrary(libraryId?: string): Observable<Library> {
        if (libraryId) {
            const params = new HttpParams().set('libraryId', libraryId);
            return this.http.get<Library>(this.apiUrl + '/libraries', { params: params });
        }

        return this.http.get<Library>(this.apiUrl + '/libraries');
    }
}
