export interface StoryModel {
    Id: string;
    Title: string;
    Description: string;
    AuthorName?: string;
    AgeLimit: number;
    ImagePreview: string;
    DatePublished: Date;
    DateWritten: Date;
    Publisher?: string;
    IsEditable: boolean;
}

export interface StoryModelWithContents extends StoryModel {
    
}