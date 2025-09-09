import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { AuthService } from '@users/services/auth.service';
import { PreviewStoryComponent } from './preview-story.component';

xdescribe('PreviewStoryComponent', () => {
    let component: PreviewStoryComponent;
    let fixture: ComponentFixture<PreviewStoryComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [PreviewStoryComponent],
            providers: [
                AuthService,
                JwtHelperService,
                { provide: JWT_OPTIONS, useValue: {} },
                provideHttpClientTesting(),
                {
                    provide: ActivatedRoute,
                    useValue: {
                        snapshot: {
                            paramMap: {
                                get: (key: string) => {
                                    if (key === 'id') return 'mocked-story-id';
                                    return null;
                                },
                            },
                        },
                    },
                },
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(PreviewStoryComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
