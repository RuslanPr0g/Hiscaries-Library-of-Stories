export interface PublishStoryRequest {
    Title: string;
    Description: string;
    AuthorName: string;
    GenreIds: string[];
    AgeLimit: number;
    ImagePreview: string;
    DateWritten: Date;
}