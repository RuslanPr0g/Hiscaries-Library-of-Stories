import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthButtonComponent } from './auth-button/auth-button.component';
import { AuthFormComponent } from './auth-form/auth-form.component';
import { AuthInputComponent } from './auth-input/auth-input.component';
import { AuthService } from '../services/auth.service';
import { take } from 'rxjs';
import { NavigationConst } from '../../shared/constants/navigation.const';

export interface ApiError {
    Message: string;
}

@Component({
    standalone: true,
    imports: [AuthButtonComponent, AuthInputComponent, AuthFormComponent, FormsModule, ReactiveFormsModule],
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    formlogin: FormGroup;
    formregister: FormGroup;
    isLoginState = true;
    errorMessage: string | null;

    username: string;

    submitted = false;

    constructor(
        private service: AuthService,
        private router: Router
    ) {
        this.formlogin = new FormGroup({
            Username: new FormControl('', Validators.required),
            Password: new FormControl('', Validators.required),
        });

        this.formregister = new FormGroup({
            Username: new FormControl('', [Validators.required, Validators.minLength(3)]),
            Password: new FormControl('', [Validators.required, Validators.minLength(3)]),
            Email: new FormControl('', [Validators.required, Validators.email]),
            Dob: new FormControl('', Validators.required),
        });
    }

    ngOnInit(): void {
        if (this.service.isAuthenticated()) {
            this.router.navigateByUrl(NavigationConst.Home);
        }
    }

    changeState(): void {
        this.isLoginState = !this.isLoginState;
        this.errorMessage = null;
        this.formlogin.reset();
        this.formregister.reset();
    }

    logIn(): void {
        if (this.formlogin?.invalid) {
            this.errorMessage = 'All fields are required!';
            return;
        }

        this.submit();
        this.service
            .login(this.formlogin?.value)
            .pipe(take(1))
            .subscribe({
                next: () => this.processAuth(),
                error: (error) => this.processError(error),
            });
    }

    signUp(): void {
        if (this.formregister?.invalid) {
            this.errorMessage = 'All fields are required and must be valid!';
            return;
        }

        this.submit();
        this.service.register(this.formregister?.value).subscribe({
            next: () => this.processAuth(),
            error: (error) => this.processError(error),
        });
    }

    private processAuth(): void {
        this.router
            .navigateByUrl(NavigationConst.Home)
            .then(() => {
                this.processed();
            })
            .catch(() => {
                this.processed();
            });
    }

    private processError(error: { error: ApiError }): void {
        this.processed();
        this.handleError(error.error);
    }

    private handleError(error: ApiError): void {
        this.errorMessage = error?.Message || 'An unexpected error occurred';
    }

    private submit(): void {
        this.errorMessage = null;
        this.submitted = true;
    }

    private processed(): void {
        this.errorMessage = null;
        this.submitted = false;
    }
}
