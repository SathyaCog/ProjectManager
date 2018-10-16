import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListModelComponent } from './task-list-model.component';

describe('TaskListModelComponent', () => {
  let component: TaskListModelComponent;
  let fixture: ComponentFixture<TaskListModelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaskListModelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
