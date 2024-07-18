import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from '../../shared/components/form-input/form-input.component';
import { FormTextareaComponent } from '../../shared/components/form-textarea/form-textarea.component';
import { FormButtonComponent } from '../../shared/components/form-button/form-button.component';

@Component({
  selector: 'app-publish-story',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormInputComponent, FormTextareaComponent, FormButtonComponent],
  templateUrl: './publish-story.component.html',
  styleUrls: ['./publish-story.component.scss']
})
export class PublishStoryComponent implements OnInit {
  publishForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.publishForm = this.fb.group({
      Title: ['', Validators.required],
      Description: ['', Validators.required]
    });
  }

  ngOnInit() {
    // If you need to perform any additional initialization, you can do it here
  }

  onSubmit() {
    if (this.publishForm.valid) {
      console.log(this.publishForm.value);
      // Implement your submit logic here
    }
  }
}
