import { AbstractControl } from '@angular/forms';

export interface ModifyLibraryFormModel {
    Bio: AbstractControl<string | null>;
    Avatar: AbstractControl<string | null>;
    LinksToSocialMedia: AbstractControl<string[] | null>;
}
