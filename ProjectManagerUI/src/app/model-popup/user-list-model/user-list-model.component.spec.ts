import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserListModelComponent } from './user-list-model.component';

describe('UserListModelComponent', () => {
  let component: UserListModelComponent;
  let fixture: ComponentFixture<UserListModelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserListModelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserListModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
