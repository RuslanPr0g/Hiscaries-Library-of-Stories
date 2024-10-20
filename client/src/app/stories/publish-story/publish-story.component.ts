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
import { PublishFormModel } from '../models/form/publish-story-form.model';
import { PublishStoryRequest } from '../models/requests/publish-story.model';
import { MessageModule } from 'primeng/message';
import { Router } from '@angular/router';

@Component({
  selector: 'app-publish-story',
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
    MessageModule
  ],
  templateUrl: './publish-story.component.html',
  styleUrls: ['./publish-story.component.scss']
})
export class PublishStoryComponent implements OnInit {
  publishForm: FormGroup<PublishFormModel>;
  genres: GenreModel[] = [];
  submitted: boolean = false;
  globalError: string | null = null;

  constructor(
    private fb: FormBuilder,
    private storyService: StoryService,
    private router: Router) {
    this.publishForm = this.fb.group<PublishFormModel>({
      Title: this.fb.control<string | null>(null, Validators.required),
      Description: this.fb.control<string | null>(null, Validators.required),
      AuthorName: this.fb.control<string | null>(null),
      Image: this.fb.control<string | null>(null),
      Genres: this.fb.control<GenreModel[] | null>(null),
      AgeLimit: this.fb.control<number | null>(null, [Validators.required, Validators.min(0), Validators.max(18)]),
      DateWritten: this.fb.control<Date | null>(null)
    });
  }

  ngOnInit() {
    this.storyService.genreList().pipe(take(1)).subscribe((genres: GenreModel[]) => {
      this.genres = genres;
    });
  }

  get imageControl(): AbstractControl<any, any> | null {
    return this.publishForm.get('Image');
  }

  get base64Image(): any {
    return this.imageControl?.value;
  }

  onSubmit() {
    if (!this.publishForm.valid) {
      return;
    }

    this.submitted = true;

    const formModel = this.publishForm.value;

    // TODO: this mapping will be useful when updating the story info, find a better place for this logic
    const request: PublishStoryRequest = {
      ...formModel,
      GenreIds: formModel.Genres?.map(g => g.Id),
      ImagePreview: formModel.Image
    };

    this.storyService.publish(request)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.submitted = false;
  
          // TODO: temporarily redirect to the home page to show "best to read stories" to see how it looks like
          // in the future we need to redirect a user to the update story page, where they would add content pages to the story
          this.router.navigateByUrl('/');
        },
        error: (error) => {
          if (error) {
            this.globalError = 'Error occured while publishing the story, please try again later';
          }

          this.submitted = false;
        }
      });
  }
}
