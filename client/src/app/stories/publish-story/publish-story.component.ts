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
    FormMultiselectComponent
],
  templateUrl: './publish-story.component.html',
  styleUrls: ['./publish-story.component.scss']
})
export class PublishStoryComponent implements OnInit {
  publishForm: FormGroup;
  genres: GenreModel[] = [];

  constructor(
    private fb: FormBuilder,
    private storyService: StoryService) {
    this.publishForm = this.fb.group({
      Title: ['', Validators.required],
      Description: ['', Validators.required],
      AuthorName: [''],
      Image: [null],
      Genres: ['', Validators.required],
      AgeLimit: [0, [Validators.required, Validators.min(0), Validators.max(18)]],
      DateWritten: [null]
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
    if (this.publishForm.valid) {
      console.log(this.publishForm.value);
    }
  }
}
