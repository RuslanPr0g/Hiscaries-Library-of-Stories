import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from '../../shared/components/form-input/form-input.component';
import { FormTextareaComponent } from '../../shared/components/form-textarea/form-textarea.component';
import { FormButtonComponent } from '../../shared/components/form-button/form-button.component';
import { NumberLimitControlComponent } from '../../shared/components/number-limit-control/number-limit-control.component';
import { FormDateInputComponent } from '../../shared/components/form-date-input/form-date-input.component';
import { DividerModule } from 'primeng/divider';
import { GenreModel } from '../models/domain/genre.model';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';
import { UploadFileControlComponent } from "../../shared/components/upload-file-control/upload-file-control.component";
import { FormMultiselectComponent } from '../../shared/components/form-multiselect/form-multiselect.component';
import { MessageModule } from 'primeng/message';
import { ActivatedRoute, Router } from '@angular/router';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ModifyFormModel } from '../models/form/modify-story-form.model';
import { ModifyStoryRequest } from '../models/requests/modify-story.model';
import { StoryModelWithContents } from '../models/domain/story-model';
import { convertToBase64 } from '../../shared/helpers/image.helper';

@Component({
  selector: 'app-modify-story',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormInputComponent,
    FormTextareaComponent,
    FormButtonComponent,
    FormDateInputComponent,
    NumberLimitControlComponent,
    DividerModule,
    UploadFileControlComponent,
    FormMultiselectComponent,
    MessageModule,
    ProgressSpinnerModule
  ],
  templateUrl: './modify-story.component.html',
  styleUrls: ['./modify-story.component.scss']
})
export class ModifyStoryComponent implements OnInit {
  private storyId: string | null = null;

  modifyForm: FormGroup<ModifyFormModel>;
  genres: GenreModel[] = [];
  submitted: boolean = false;
  globalError: string | null = null;

  story: StoryModelWithContents | null = null;
  storyNotFound: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private storyService: StoryService,
    private router: Router) {
    this.modifyForm = this.fb.group<ModifyFormModel>({
      Title: this.fb.control<string | null>(null, Validators.required),
      Description: this.fb.control<string | null>(null, Validators.required),
      AuthorName: this.fb.control<string | null>(null, Validators.required),
      Image: this.fb.control<string | null>(null, Validators.required),
      Genres: this.fb.control<GenreModel[] | null>(null, Validators.required),
      AgeLimit: this.fb.control<number | null>(null, [Validators.required, Validators.min(0), Validators.max(18)]),
      DateWritten: this.fb.control<Date | null>(null, Validators.required),
      Contents: this.fb.control<string[] | null>(null, Validators.required)
    });

    this.storyId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    if (!this.storyId) {
      this.storyNotFound = true;
      return;
    }

    this.storyService.genreList()
      .pipe(take(1))
      .subscribe((genres: GenreModel[]) => {
        this.genres = genres;
      });

    this.storyService.getStoryByIdWithContents({
      Id: this.storyId
    })
      .pipe(take(1))
      .subscribe({
        next: story => {
          if (!story) {
            this.storyNotFound = true;
          } else {
            this.story = {
              ...story,
              ImagePreview: convertToBase64(story.ImagePreview)
            };
          }

          this.populateFormWithValue();
        },
        error: () => this.storyNotFound = true
      });
  }

  get imageControl(): AbstractControl<any, any> | null {
    return this.modifyForm.get('Image');
  }

  get base64Image(): any {
    return this.imageControl?.value;
  }

  onSubmit() {
    if (!this.modifyForm.valid) {
      this.modifyForm.markAllAsTouched();
      return;
    }

    this.submitted = true;

    const formModel = this.modifyForm.value;

    const request: ModifyStoryRequest = {
      ...formModel,
      GenreIds: formModel.Genres?.map(g => g.Id),
      ImagePreview: formModel.Image,
      Contents: formModel.Contents ?? []
    };

    this.storyService.modify(request)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.router.navigateByUrl(`/preview-story/${this.storyId}`);
        },
        error: (error) => {
          if (error) {
            this.globalError = 'Error occured while modifying the story, please try again later';
          }

          this.submitted = false;
        }
      });
  }

  private populateFormWithValue(): void {
    console.warn(this.story);
    if (this.story) {
      this.modifyForm.patchValue({
        Title: this.story.Title,
        Description: this.story.Description,
        AuthorName: this.story.AuthorName,
        Image: this.story.ImagePreview,
        Genres: this.story.Genres,
        AgeLimit: this.story.AgeLimit,
        DateWritten: this.story.DateWritten,
        Contents: this.story.Contents,
      });
    }
  }
}