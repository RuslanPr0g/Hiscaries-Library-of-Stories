import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    standalone: true,
    selector: 'app-auth-button',
    host: {
        '[class.disabled]': 'disabled',
    },
    templateUrl: './auth-button.component.html',
    styleUrls: ['./auth-button.component.scss'],
})
export class AuthButtonComponent {
    @Input() type: string = 'button';
    @Input() disabled: boolean = false;

    @Output() clicked = new EventEmitter<void>();

    clickedAction(): void {
        this.clicked?.emit();
    }
}
