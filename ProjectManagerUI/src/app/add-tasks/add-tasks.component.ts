import { Component, OnInit } from '@angular/core';
import { TaskModel } from '../models/task-model';
import { ProjectModel } from '../models/project-model';
import { UserModel } from '../models/user-model';
import { ParentTaskModel } from '../models/task-model';
import { ApiService } from '../service/api-service';
import { DialogService } from "ng2-bootstrap-modal";
import { UserListModelComponent } from '../model-popup/user-list-model/user-list-model.component';
import { ProjectListModelComponent } from '../model-popup/project-list-model/project-list-model.component';


@Component({
  selector: 'app-add-tasks',
  templateUrl: './add-tasks.component.html',
  styleUrls: ['./add-tasks.component.css']
})
export class AddTasksComponent implements OnInit {
  IsParentTask: boolean;
  StartDate: string;
  EndDate: string;
  Priority: number;
  ParentTask: string;
  ParentTaskID: number;
  UserID: number;
  UserName: string;
  Project: string;
  ProjectID: number;
  Task: string;
  TaskID: number;
  taskModel: TaskModel;
  parentTaskModel: ParentTaskModel;
  projectList: ProjectModel[];
  UserList: UserModel[];

  constructor(private apiService: ApiService, private dialogService: DialogService) {
    this.Priority = 0;
    this.StartDate = new Date().toISOString().split('T')[0];
    let tmpDate = new Date();
    tmpDate.setDate(tmpDate.getDate() + 1);
    this.EndDate = tmpDate.toISOString().split('T')[0];
  }

  ngOnInit() {
  }

  TaskCheckBoxChange() {
    if (this.IsParentTask) {

      document.getElementById('projDialogButton').style.display = 'none';
      document.getElementById('parentTaskDialogButton').style.display = 'none';
      document.getElementById('openUserDialogButton').style.display = 'none';
      this.StartDate = undefined;
      this.EndDate = undefined;
      this.ProjectID = undefined;
      this.Priority = 0;
      this.ParentTaskID = undefined;
      this.UserID = undefined;
    }
    else {
      document.getElementById('projDialogButton').style.display = 'block';
      document.getElementById('parentTaskDialogButton').style.display = 'block';
      document.getElementById('openUserDialogButton').style.display = 'block';
      this.StartDate = new Date().toISOString().split('T')[0];
      let tmpDate = new Date();
      tmpDate.setDate(tmpDate.getDate() + 1);
      this.EndDate = tmpDate.toISOString().split('T')[0];
    }
  }

  AddUpdateTask() {
    if (this.ProjectID) {
      this.AddTask();
    }
    else {
      this.AddTask();
    }
  }

  AddTask() {
    if (this.IsParentTask) {
      this.parentTaskModel = new ParentTaskModel();
      this.parentTaskModel.ParentTask = this.Task;

      this.apiService.AddParentTask(this.parentTaskModel)
        .subscribe((data: any) => {
          console.log(data);
        },
          function (error) {
            console.log(error);
          });
    }
    else {
      this.taskModel = new TaskModel();
      this.taskModel.Task = this.Task;
      this.taskModel.ProjectID = this.ProjectID;
      this.taskModel.Project = this.Project;
      this.taskModel.Priority = this.Priority;
      this.taskModel.StartDate = new Date(this.StartDate);
      this.taskModel.EndDate = new Date(this.EndDate);
      this.taskModel.UserID = this.UserID;
      this.apiService.AddTask(this.taskModel)
        .subscribe((data: any) => {
          console.log(data);
        },
          function (error) {
            console.log(error);
          });
    }
  }

  openProjectDialog() {
    let disposable = this.dialogService.addDialog(ProjectListModelComponent, this.projectList)
      .subscribe((selectedProject) => {
        if (selectedProject) {
          this.ProjectID = selectedProject.ProjectID;
          this.Project = selectedProject.Project;
        }
      });
    setTimeout(() => {
      disposable.unsubscribe();
    }, 10000);
  }

  openParentTaskDialog() {

  }

  openUserDialog() {
    let disposable = this.dialogService.addDialog(UserListModelComponent, this.UserList)
      .subscribe((selectedUser) => {
        if (selectedUser) {
          this.UserID = selectedUser.UserID;
          this.UserName = selectedUser.FirstName + ' ' + selectedUser.LastName;
        }
      });
    setTimeout(() => {
      disposable.unsubscribe();
    }, 10000);
  }

}
