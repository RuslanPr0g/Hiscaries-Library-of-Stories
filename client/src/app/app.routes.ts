import { Routes } from '@angular/router';
import { LoginComponent } from './users/login/login.component';
import { HomeComponent } from './users/home/home.component';
import { authGuard } from './shared/auth/guards/auth.guard';
import { provideState } from '@ngrx/store';
import { userFeatureKey, userReducer } from './users/store/user.reducer';
import { PublishStoryComponent } from './stories/publish-story/publish-story.component';
import { PreviewStoryComponent } from './stories/preview-story/preview-story.component';

export const routes: Routes = [
    {
        path: 'login',
        title: 'Login',
        component: LoginComponent,
        providers: [
            provideState({ name: userFeatureKey, reducer: userReducer })
          ]
    },
    {
        path: '',
        title: 'Home page',
        component: HomeComponent, 
        canActivate: [authGuard]
    },
    {
        path: 'publish-story',
        title: 'Publish Story',
        component: PublishStoryComponent,
        canActivate: [authGuard]
    },
    {
        path: 'preview-story/:id',
        title: 'Preview Story',
        component: PreviewStoryComponent,
        canActivate: [authGuard]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
