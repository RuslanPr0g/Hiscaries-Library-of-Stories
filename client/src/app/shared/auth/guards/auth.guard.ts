import { inject } from "@angular/core";
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { UserService } from "../../../users/services/user.service";

export const authGuard: CanActivateFn = (
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) => {
        if (inject(UserService).isAuthenticated()) {
            return true;
        }

        console.warn('User is not authenticated');
        inject(Router).navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
  }