import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const accessTokenKey = environment.localStorageKeys.ACCESS_TOKEN_KEY;
    const token = localStorage.getItem(accessTokenKey);
    
    const excludedEndpoints = ['/login', '/register'];

    console.warn('Request URL:', req.url);

    if (token && !excludedEndpoints.some(endpoint => req.url.includes(endpoint))) {
        const cloned = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
        return next(cloned);
    }

    return next(req);
};
