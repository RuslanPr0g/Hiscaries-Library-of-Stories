import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from '../../shared/components/form-input/form-input.component';
import { FormTextareaComponent } from '../../shared/components/form-textarea/form-textarea.component';
import { FormButtonComponent } from '../../shared/components/form-button/form-button.component';
import { NumberLimitControlComponent } from '../../shared/components/number-limit-control/number-limit-control.component';
import { FormDateInputComponent } from '../../shared/components/form-date-input/form-date-input.component';
import { DividerModule } from 'primeng/divider';
import { GenreModel } from '../models/domain/genre.model';
import { MultiSelectModule } from 'primeng/multiselect';
import { StoryService } from '../services/story.service';
import { take } from 'rxjs';

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
    MultiSelectModule
  ],
  templateUrl: './publish-story.component.html',
  styleUrls: ['./publish-story.component.scss']
})
export class PublishStoryComponent implements OnInit {
  publishForm: FormGroup;
  genres: GenreModel[] = [];

  selectedFile: any;
  base64Image: string | ArrayBuffer | null = null;

  constructor(
    private fb: FormBuilder,
    private storyService: StoryService) {
    this.publishForm = this.fb.group({
      Title: ['', Validators.required],
      Description: ['', Validators.required],
      AuthorName: [''],
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

  onSubmit() {
    if (this.publishForm.valid) {
      console.log(this.publishForm.value);
      // Implement your submit logic here
    }
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;

    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];

      const reader = new FileReader();
      reader.onload = () => {
        // The result is the base64 string
        this.base64Image = reader.result;
        
        // Patch the base64Image to the form
        this.publishForm.patchValue({
          Image: this.base64Image
        });
      };

      reader.readAsDataURL(this.selectedFile);  // Convert to base64
    }
  }
}
