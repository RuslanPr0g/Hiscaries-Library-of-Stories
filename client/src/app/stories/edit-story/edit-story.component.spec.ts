import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStoryComponent } from './edit-story.component';

describe('EditStoryComponent', () => {
  let component: EditStoryComponent;
  let fixture: ComponentFixture<EditStoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditStoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditStoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
