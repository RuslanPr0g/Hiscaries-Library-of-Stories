import { createSelector } from '@ngrx/store';
import { StoryStateModel } from './story-state.model';

export const searchTerm = (state: StoryStateModel) => state.SearchTerm;

export const searchSearchTerm = createSelector(searchTerm, (state: string | null) => state);
