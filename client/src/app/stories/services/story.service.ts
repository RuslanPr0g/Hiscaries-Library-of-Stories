import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PublishStoryRequest } from '../models/requests/publish-story.model';
import { BaseIdModel } from '../../shared/models/base-id.model';

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private apiUrl = `${environment.apiUrl}/stories`;

  constructor(private http: HttpClient) {}

  publish(request: PublishStoryRequest): Observable<BaseIdModel> {
    return this.http.post<BaseIdModel>(this.apiUrl, request);
  }
}
