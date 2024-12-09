import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReadStoryContentComponent } from './read-story-content.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { AuthService } from '../../users/services/auth.service';
import { ActivatedRoute } from '@angular/router';

xdescribe('ReadStoryContentComponent', () => {
    let component: ReadStoryContentComponent;
    let fixture: ComponentFixture<ReadStoryContentComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [ReadStoryContentComponent],
            providers: [
                AuthService,
                JwtHelperService,
                { provide: JWT_OPTIONS, useValue: {} },
                { provide: ActivatedRoute, useValue: { snapshot: { paramMap: {} } } },
                provideHttpClient(),
                provideHttpClientTesting(),
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(ReadStoryContentComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
