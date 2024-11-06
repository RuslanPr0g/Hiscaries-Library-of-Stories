import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withRouterConfig } from '@angular/router';
import { routes } from './app.routes';
import { provideState, provideStore } from '@ngrx/store';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';
import { storyFeatureKey, storyReducer } from './stories/store/story.reducer';

export function tokenGetter() {
    return localStorage.getItem(environment.localStorageKeys.ACCESS_TOKEN_KEY);
}

export const appConfig: ApplicationConfig = {
    providers: [
        provideZoneChangeDetection({ eventCoalescing: true }),
        provideRouter(
            routes,
            withRouterConfig({
                paramsInheritanceStrategy: 'always',
                onSameUrlNavigation: 'reload',
            })
        ),
        provideStore(),
        importProvidersFrom(
            JwtModule.forRoot({
                config: {
                    tokenGetter: tokenGetter,
                    // TODO: make this thing come from CD/CI
                    allowedDomains: ['localhost'],
                    disallowedRoutes: ['/auth/login', '/auth/register'],
                },
            })
        ),
        provideHttpClient(withInterceptorsFromDi()),
        provideState({ name: storyFeatureKey, reducer: storyReducer }),
        provideAnimationsAsync(),
    ],
};
