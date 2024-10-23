import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { environment } from '../../../environments/environment';
import { RegisterUserRequest } from "../models/requests/register-user.model";
import { UserWithTokenResponse } from "../models/response/user-with-token.model";
import { LoginUserRequest } from "../models/requests/login-user.model";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl: string = `${environment.apiUrl}/users`;
  private access_token_local_storage_key: string = environment.localStorageKeys.ACCESS_TOKEN_KEY;
  private refresh_token_local_storage_key: string = environment.localStorageKeys.REFRESH_TOKEN_KEY;

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService
  ) {
  }

  register(request: RegisterUserRequest): Observable<UserWithTokenResponse> {
    return this.http.post(this.apiUrl + "/register", request)
      .pipe(
        tap((tokenData: any) => {
          localStorage.setItem(this.access_token_local_storage_key, tokenData.Token);
          localStorage.setItem(this.refresh_token_local_storage_key, tokenData.RefreshToken);
        })
      );
  }

  login(request: LoginUserRequest): Observable<UserWithTokenResponse> {
    return this.http.post(this.apiUrl + "/login", request)
      .pipe(
        tap((tokenData: any) => {
          localStorage.setItem(this.access_token_local_storage_key, tokenData.Token);
          localStorage.setItem(this.refresh_token_local_storage_key, tokenData.RefreshToken);
        })
      );
  }

  logOut(): void {
    localStorage.removeItem(this.access_token_local_storage_key);
    localStorage.removeItem(this.refresh_token_local_storage_key);
  }

  isAuthenticated(): boolean {
    let token = localStorage.getItem(this.access_token_local_storage_key);
    let authenticated = token != null && !this.jwtHelper.isTokenExpired(token);
    if (!authenticated) {
      this.logOut();
      return false;
    }

    return true;
  }

  isTokenOwner(userId?: string): boolean {
    const token = localStorage.getItem(this.access_token_local_storage_key);

    if (!token) {
      return false;
    }

    const decodedToken = this.jwtHelper.decodeToken(token);

    const id = decodedToken?.id;

    return id === userId;
  }
}