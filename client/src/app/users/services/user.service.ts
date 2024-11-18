import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private apiUrl: string = `${environment.apiUrl}/users`;

    constructor(private http: HttpClient) {}

    becomePublisher(): Observable<void> {
        return this.http.post<void>(this.apiUrl + '/become-publisher', {});
    }
}
