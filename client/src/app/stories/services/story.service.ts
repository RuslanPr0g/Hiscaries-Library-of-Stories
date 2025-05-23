import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PublishStoryRequest } from '../models/requests/publish-story.model';
import { BaseIdModel } from '../../shared/models/base-id.model';
import { GenreModel } from '../models/domain/genre.model';
import { StoryModel, StoryModelWithContents } from '../models/domain/story-model';
import { SearchStoryRequest } from '../models/requests/search-story.model';
import { ModifyStoryRequest } from '../models/requests/modify-story.model';
import { SearchStoryWithContentsRequest } from '../models/requests/search-story-with-contents.model';

@Injectable({
    providedIn: 'root',
})
export class StoryService {
    private apiUrl = `${environment.apiUrl}/stories`;

    constructor(private http: HttpClient) {}

    genreList(): Observable<GenreModel[]> {
        return this.http.get<GenreModel[]>(`${this.apiUrl}/genres`);
    }

    recommendations(): Observable<StoryModel[]> {
        return this.http.get<StoryModel[]>(`${this.apiUrl}/recommendations`);
    }

    getStoriesByLibraryId(libraryId: string): Observable<StoryModel[]> {
        const params = new HttpParams().set('libraryId', libraryId);
        return this.http.get<StoryModel[]>(`${this.apiUrl}/`, { params });
    }

    searchStory(request: SearchStoryRequest): Observable<StoryModel[]> {
        return this.http.post<StoryModel[]>(`${this.apiUrl}/search`, request);
    }

    getStoryByIdWithContents(request: SearchStoryWithContentsRequest): Observable<StoryModelWithContents> {
        return this.http.post<StoryModelWithContents>(`${this.apiUrl}/by-id-with-contents`, request);
    }

    publish(request: PublishStoryRequest): Observable<BaseIdModel> {
        return this.http.post<BaseIdModel>(`${this.apiUrl}`, request);
    }

    modify(request: ModifyStoryRequest): Observable<void> {
        return this.http.patch<void>(`${this.apiUrl}`, request);
    }
}
