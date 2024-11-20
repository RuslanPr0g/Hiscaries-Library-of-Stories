import { UserModel } from './user.model';

export interface Library {
    PlatformUser: UserModel;
    Id: string;
    Bio: string;
    AvatarUrl: string;
    LinksToSocialMedia: string[];

    IsLibraryOwner: boolean;
}
