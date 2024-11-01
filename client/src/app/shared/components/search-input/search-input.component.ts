import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DestroyService } from '../../services/destroy.service';

@Component({
    selector: 'app-search-input',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './search-input.component.html',
    styleUrls: ['./search-input.component.scss'],
    providers: [DestroyService],
})
export class SearchInputComponent implements AfterViewInit {
    @ViewChild('searchValue') searchValueRef!: ElementRef;

    @Input() defaultValue?: string | null;

    @Output() searchAction = new EventEmitter<string>();

    isHighlighted: boolean = true;

    constructor(
        private destroy: DestroyService,
        private route: ActivatedRoute
    ) {
        setTimeout(() => (this.isHighlighted = false), 15000);
    }

    ngAfterViewInit(): void {
        if (this.searchValueRef?.nativeElement) {
            this.searchValueRef.nativeElement.value = this.defaultValue;
        }
    }

    search(term: string): void {
        this.searchAction?.emit(term);
    }
}
