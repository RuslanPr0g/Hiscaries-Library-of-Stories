import { UserModel } from '../../../users/models/domain/user.model';
import { GenreModel } from './genre.model';

export interface StoryModel {
    Id: string;
    Title: string;
    Description: string;
    AuthorName?: string;
    AgeLimit: number;
    ImagePreviewUrl: string;
    DatePublished: Date;
    DateWritten: Date;
    Publisher?: UserModel;
    IsEditable: boolean;
}

export interface StoryModelWithContents extends StoryModel {
    Genres: GenreModel[];
    Contents: StoryPageModel[];
}

export interface StoryPageModel {
    Page: number;
    Content: string;
}
