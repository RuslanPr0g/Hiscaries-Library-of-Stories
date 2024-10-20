import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, ReactiveFormsModule } from '@angular/forms';
import { MessageModule } from 'primeng/message';
import { FileUploadModule, UploadEvent } from 'primeng/fileupload';

@Component({
  selector: 'app-upload-file-control',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FileUploadModule, MessageModule],
  templateUrl: './upload-file-control.component.html',
  styleUrls: ['./upload-file-control.component.scss']
})
export class UploadFileControlComponent {
  @Input() control: AbstractControl<any, any> | null;
  @Input() centered: boolean = false;

  onSelect(event: any) {
    if (!this.control) {
      console.error('No control to upload a file was provided. Skipping file upload.');
      return;
    }

    const file = event.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      this.updateControlValue(reader);
    };
    reader.readAsDataURL(file);
  }

  onUpload(event: UploadEvent) {
   this.onSelect(event);
  }

  private updateControlValue(reader: FileReader): void {
    this.control?.patchValue(reader.result);
  }
}
