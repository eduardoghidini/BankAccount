import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationRequestListComponent } from './operation-request-list.component';

describe('OperationRequestListComponent', () => {
  let component: OperationRequestListComponent;
  let fixture: ComponentFixture<OperationRequestListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OperationRequestListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OperationRequestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
