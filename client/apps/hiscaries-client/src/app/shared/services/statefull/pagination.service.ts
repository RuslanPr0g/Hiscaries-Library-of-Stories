import { Injectable } from '@angular/core';
import { QueryableModel } from '@shared/models/queryable.model';
import { BehaviorSubject, distinctUntilChanged } from 'rxjs';

@Injectable()
export class PaginationService {
    private state$ = new BehaviorSubject<QueryableModel>({
        StartIndex: 0,
        ItemsCount: 5,
        SortProperty: 'CreatedAt',
        SortAsc: false,
    });

    private loading$ = new BehaviorSubject<boolean>(false);

    get query$() {
        return this.state$.asObservable().pipe(distinctUntilChanged((a, b) => JSON.stringify(a) === JSON.stringify(b)));
    }

    get isLoading$() {
        return this.loading$.asObservable();
    }

    get snapshot() {
        return this.state$.value;
    }

    setPage(start: number, count: number) {
        this.state$.next({ ...this.snapshot, StartIndex: start, ItemsCount: count });
    }

    setSort(property: string, asc: boolean) {
        this.state$.next({ ...this.snapshot, SortProperty: property, SortAsc: asc });
    }

    nextPage() {
        const { StartIndex, ItemsCount } = this.snapshot;
        this.setPage(StartIndex + ItemsCount, ItemsCount);
    }

    prevPage() {
        const { StartIndex, ItemsCount } = this.snapshot;
        this.setPage(Math.max(0, StartIndex - ItemsCount), ItemsCount);
    }

    reset() {
        this.state$.next({
            StartIndex: 0,
            ItemsCount: 5,
            SortProperty: 'CreatedAt',
            SortAsc: false,
        });
    }

    setLoading(isLoading: boolean) {
        this.loading$.next(isLoading);
    }
}
