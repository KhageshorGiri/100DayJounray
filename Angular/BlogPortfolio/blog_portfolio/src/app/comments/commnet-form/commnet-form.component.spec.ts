import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommnetFormComponent } from './commnet-form.component';

describe('CommnetFormComponent', () => {
  let component: CommnetFormComponent;
  let fixture: ComponentFixture<CommnetFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommnetFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CommnetFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
