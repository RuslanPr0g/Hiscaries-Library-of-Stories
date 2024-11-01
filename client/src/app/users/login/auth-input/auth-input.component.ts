import { Component, forwardRef, Input, Provider } from '@angular/core';
import { ControlContainer, ControlValueAccessor, FormGroupDirective, NG_VALUE_ACCESSOR } from '@angular/forms';

export const CUSTOM_CONROL_VALUE_ACCESSOR: Provider = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => AuthInputComponent),
    multi: true,
};
@Component({
    standalone: true,
    selector: 'app-auth-input',
    templateUrl: './auth-input.component.html',
    styleUrls: ['./auth-input.component.scss'],
    providers: [CUSTOM_CONROL_VALUE_ACCESSOR],
    viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }],
})
export class AuthInputComponent implements ControlValueAccessor {
    @Input({ required: true }) formControlName?: string;
    @Input() type: string = 'text';
    @Input() placeholder?: string;
    value: string;

    onChange: (value: string) => void = () => {};
    onTouched: () => void = () => {};

    writeValue(value: string): void {
        this.value = value;
    }

    registerOnChange(fn: (value: string) => void): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: () => void): void {
        this.onTouched = fn;
    }

    onInput(event: Event): void {
        const inputElement = event.target as HTMLInputElement;
        this.value = inputElement.value;
        this.onChange(this.value);
    }
}
