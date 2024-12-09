import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { PublishStoryComponent } from './publish-story.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { AuthService } from '../../users/services/auth.service';
import { StoryService } from '../services/story.service';

xdescribe('PublishStoryComponent', () => {
    let component: PublishStoryComponent;
    let fixture: ComponentFixture<PublishStoryComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [PublishStoryComponent, CommonModule, ReactiveFormsModule],
            providers: [
                AuthService,
                StoryService,
                JwtHelperService,
                { provide: JWT_OPTIONS, useValue: {} },
                provideHttpClient(),
                provideHttpClientTesting(),
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(PublishStoryComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
