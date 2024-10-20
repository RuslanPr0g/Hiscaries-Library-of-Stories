import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PublishStoryRequest } from '../models/requests/publish-story.model';
import { BaseIdModel } from '../../shared/models/base-id.model';
import { GenreModel } from '../models/domain/genre.model';
import { StoryModel } from '../models/domain/story-model';

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private apiUrl = `${environment.apiUrl}/stories`;

  constructor(private http: HttpClient) {}

  genreList(): Observable<GenreModel[]> {
    return this.http.get<GenreModel[]>(`${this.apiUrl}/genres`);
  }

  publish(request: PublishStoryRequest): Observable<BaseIdModel> {
    return this.http.post<BaseIdModel>(`${this.apiUrl}/publish`, request);
  }

  recommendations(): Observable<StoryModel[]> {
    return this.http.get<StoryModel[]>(`${this.apiUrl}/recommendations`);
  }
}
