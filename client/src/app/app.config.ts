import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideState, provideStore } from '@ngrx/store';
import { userFeatureKey, userReducer } from './users/store/user.reducer';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';

export function tokenGetter() {
    return localStorage.getItem(environment.localStorageKeys.ACCESS_TOKEN_KEY);
}

export const appConfig: ApplicationConfig = {
    providers: [
        provideZoneChangeDetection({ eventCoalescing: true }),
        provideRouter(routes),
        provideStore(),
        importProvidersFrom(
            JwtModule.forRoot({
                config: {
                    tokenGetter: tokenGetter,
                    allowedDomains: [environment.apiDomain],
                    disallowedRoutes: [`${environment.apiDomain}/login`, `${environment.apiDomain}/register`],
                },
            })
        ),
        provideHttpClient(withInterceptorsFromDi()),
        provideState({ name: userFeatureKey, reducer: userReducer }),
        provideAnimationsAsync(),
    ],
};
