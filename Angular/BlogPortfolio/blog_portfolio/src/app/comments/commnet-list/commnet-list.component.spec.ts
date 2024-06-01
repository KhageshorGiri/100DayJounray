import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommnetListComponent } from './commnet-list.component';

describe('CommnetListComponent', () => {
  let component: CommnetListComponent;
  let fixture: ComponentFixture<CommnetListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommnetListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CommnetListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
