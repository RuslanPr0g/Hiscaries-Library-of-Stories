import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';

@Component({
  selector: 'app-form-date-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, CalendarModule],
  templateUrl: './form-date-input.component.html',
  styleUrls: ['./form-date-input.component.scss']
})
export class FormDateInputComponent {
  @Input() formGroup!: FormGroup;
  @Input() controlName!: string;
  @Input() label!: string;
  @Input() errorMessage!: string;
}
