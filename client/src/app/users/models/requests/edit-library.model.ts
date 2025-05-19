export interface EditLibraryRequest {
    LibraryId: string;
    Bio: string;
    Avatar: string;
    ShouldUpdateAvatar: boolean;
    LinksToSocialMedia: string[];
}
