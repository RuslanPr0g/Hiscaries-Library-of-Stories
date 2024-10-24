import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageModule } from 'primeng/message';
import { EditorModule } from 'primeng/editor';
import { TextEditorComponent } from '../../../shared/components/text-editor/text-editor.component';
import { FormButtonComponent } from '../../../shared/components/form-button/form-button.component';

@Component({
  selector: 'app-content-builder',
  standalone: true,
  imports: [CommonModule, EditorModule, ReactiveFormsModule, MessageModule, FormsModule, TextEditorComponent, FormButtonComponent],
  templateUrl: './content-builder.component.html',
  styleUrls: ['./content-builder.component.scss']
})
export class ContentBuilderComponent {
  @Input() formGroup: FormGroup;
  @Input() formArrayName: string;
  @Input() contents: FormArray;

  constructor(
    private fb: FormBuilder
  ) { }

  getContentControlByIndex(index: number): AbstractControl<string> {
    return this.contents.at(index);
  }

  addContent() {
    this.contents.push(this.fb.control(''));
  }

  removeContent(index: number) {
    this.contents.removeAt(index);
  }
}
