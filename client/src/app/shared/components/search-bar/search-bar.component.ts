import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DestroyService } from '../../services/destroy.service';
import { Observable } from 'rxjs';
import { NavigationConst } from '../../constants/navigation.const';
import { Router } from '@angular/router';
import { StoryStateModel } from '../../../stories/store/story-state.model';
import { Store } from '@ngrx/store';
import { searchSearchTerm } from '../../../stories/store/story.selector';
import { SearchInputComponent } from '../search-input/search-input.component';
import { NotificationStateService } from '../../services/statefull/notification-state.service';

@Component({
    selector: 'app-search-bar',
    standalone: true,
    imports: [CommonModule, SearchInputComponent],
    templateUrl: './search-bar.component.html',
    styleUrls: ['./search-bar.component.scss'],
    providers: [DestroyService],
})
export class SearchBarComponent implements OnInit {
    unreadCount = 0;

    searchTerm$: Observable<string | null>;

    constructor(
        private router: Router,
        private store: Store<StoryStateModel>,
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        private notificationStateService: NotificationStateService<any>
    ) {
        this.searchTerm$ = this.store.select(searchSearchTerm);
    }

    ngOnInit(): void {
        // TODO: I do not fkcing know why it doesnt work, Im giving up
        setTimeout(() => this.searchTerm$.subscribe((value) => console.warn('searchstory JERE@!!!!!', value)), 3000);

        this.notificationStateService.unreadCount$.subscribe((count) => {
            console.warn('NOTIFICATION STATE RECEIVED ON SEARCH BAR (bell icon)');
            this.unreadCount = count;
        });
    }

    search(term: string): void {
        this.router.navigate([NavigationConst.SearchStory(term)]);
    }
}
