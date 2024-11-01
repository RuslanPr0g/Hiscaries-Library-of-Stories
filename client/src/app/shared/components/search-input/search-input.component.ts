import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-search-input',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './search-input.component.html',
    styleUrls: ['./search-input.component.scss'],
})
export class SearchInputComponent {
    @Output() searchAction = new EventEmitter<string>();

    search(term: string): void {
        this.searchAction?.emit(term);
    }
}
