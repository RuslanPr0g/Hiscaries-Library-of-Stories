import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import { AuthService } from '@users/services/auth.service';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthButtonComponent } from './auth-button/auth-button.component';
import { AuthFormComponent } from './auth-form/auth-form.component';
import { AuthInputComponent } from './auth-input/auth-input.component';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';

describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                LoginComponent,
                AuthButtonComponent,
                AuthInputComponent,
                AuthFormComponent,
                FormsModule,
                ReactiveFormsModule,
            ],
            providers: [
                AuthService,
                JwtHelperService,
                { provide: JWT_OPTIONS, useValue: {} },
                provideHttpClient(),
                provideHttpClientTesting(),
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
