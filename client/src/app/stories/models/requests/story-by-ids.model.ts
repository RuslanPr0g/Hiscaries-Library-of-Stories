import { SortableRequest } from '../../../shared/models/sortable-request.model';

export interface SearchStoryByIdsRequest {
    Ids: string[];
    Sorting: SortableRequest;
}
