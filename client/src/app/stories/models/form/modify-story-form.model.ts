import { AbstractControl } from "@angular/forms";
import { GenreModel } from "../domain/genre.model";

export interface ModifyFormModel {
  Title: AbstractControl<string | null>;
  Description: AbstractControl<string | null>;
  AuthorName: AbstractControl<string | null>;
  Image: AbstractControl<string | null>;
  Genres: AbstractControl<GenreModel[] | null>;
  AgeLimit: AbstractControl<number | null>;
  DateWritten: AbstractControl<Date | null>;
  Contents: AbstractControl<string[] | null>;
}