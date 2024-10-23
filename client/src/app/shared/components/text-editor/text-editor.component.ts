import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageModule } from 'primeng/message';
import { EditorModule } from 'primeng/editor';

@Component({
  selector: 'app-text-editor',
  standalone: true,
  imports: [CommonModule, EditorModule, ReactiveFormsModule, MessageModule, FormsModule],
  templateUrl: './text-editor.component.html',
  styleUrls: ['./text-editor.component.scss']
})
export class TextEditorComponent {
  @Input() control!: AbstractControl<string>;
  @Input() label!: string;
  @Input() errorMessage!: string;

  set text(value: string) {
    console.warn('text updated: ', value);
    this.control.setValue(value);
  }

  ngOnInit(): void {
    console.warn(this.control);
  }
}
