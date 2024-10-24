import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageModule } from 'primeng/message';
import { EditorModule } from 'primeng/editor';
import { TextEditorComponent } from '../../../shared/components/text-editor/text-editor.component';
import { FormButtonComponent } from '../../../shared/components/form-button/form-button.component';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-content-builder',
  standalone: true,
  imports: [CommonModule, EditorModule, ButtonModule, ReactiveFormsModule, MessageModule, FormsModule, TextEditorComponent, FormButtonComponent],
  templateUrl: './content-builder.component.html',
  styleUrls: ['./content-builder.component.scss']
})
export class ContentBuilderComponent implements OnInit {
  private _currentIndex: number = 0;

  @Input() formGroup: FormGroup;
  @Input() formArrayName: string;
  @Input() contents: FormArray;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    if (this.contents.length === 0) {
      this.addContent();
    }
  }

  get currentIndex(): number {
    return this._currentIndex;
  }

  get currentPageControl(): AbstractControl<string> {
    return this.contents.at(this._currentIndex);
  }

  get currentPageLabel(): string {
    return `Page: ${(this._currentIndex + 1)} / ${this.contents.length}`;
  }

  moveNext(): boolean {
    if (this._currentIndex === this.contents.length - 1) {
      return false;
    }

    this._currentIndex++;

    return true;
  }

  movePrev(): boolean {
    if (this._currentIndex === 0) {
      return false;
    }

    this._currentIndex--;

    return true;
  }

  addContent() {
    this.contents.push(this.fb.control(''));
    this._currentIndex = this.contents.length - 1;
  }

  removeContent() {
    if (this.contents.length <= 1) {
      return;
    }

    this.contents.removeAt(this._currentIndex);
    this.movePrev();
  }
}
