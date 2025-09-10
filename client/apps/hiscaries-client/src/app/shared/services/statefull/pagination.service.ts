import { Injectable, signal } from '@angular/core';
import { QueryableModel } from '@shared/models/queryable.model';

@Injectable()
export class PaginationService {
    private state = signal<QueryableModel>({
        StartIndex: 0,
        ItemsCount: 5,
        SortProperty: 'CreatedAt',
        SortAsc: false,
    });

    private loading = signal<boolean>(false);

    get query() {
        return this.state;
    }

    get isLoading() {
        return this.loading;
    }

    get snapshot() {
        return this.state();
    }

    setPage(start: number, count: number) {
        this.state.update((current) => ({ ...current, StartIndex: start, ItemsCount: count }));
    }

    setSort(property: string, asc: boolean) {
        this.state.update((current) => ({ ...current, SortProperty: property, SortAsc: asc }));
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
        this.state.set({
            StartIndex: 0,
            ItemsCount: 5,
            SortProperty: 'CreatedAt',
            SortAsc: false,
        });
    }

    setLoading(isLoading: boolean) {
        this.loading.set(isLoading);
    }
}
