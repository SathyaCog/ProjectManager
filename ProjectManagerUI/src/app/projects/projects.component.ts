import { Component, OnInit } from '@angular/core';
import { ProjectModel } from '../models/project-model';
import { UserModel } from '../models/user-model';
import { ApiService } from '../service/api-service';
import { DialogService } from "ng2-bootstrap-modal";
import { UserListModelComponent } from '../user-list-model/user-list-model.component';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  ProjectID: number;
  Project: string;
  StartDate: string;
  EndDate: string;
  Priority: number;
  UserID: number;
  ManagerName: string;
  NoofTasks: number;
  NoofCompletedTasks: number;
  StartEndDateSelected: boolean;
  object: ProjectModel;
  projectList: ProjectModel[];
  filteredList: ProjectModel[];
  projListCount: number;
  AddButtonText: string;
  ResetButtonText: string;
  startMinDate: string;
  endMinDate: string;
  UserList: UserModel[];

  constructor(private apiService: ApiService, private dialogService: DialogService) {
    this.StartEndDateSelected = false;
    this.Priority = 0;
  }

  ngOnInit() {
    this.AddButtonText = "Add";
    this.ResetButtonText = "Reset";
    this.GetProjects();
    this.GetUsers();
  }

  GetProjects() {
    this.apiService.GetProjects()
      .subscribe((data: ProjectModel[]) => {
        console.log(data);
        this.projectList = data;
        this.assignCopy();
      },
        function (error) {
          console.log(error);
        });
  }

  AddUpdateProject() {
    if (this.ProjectID) {
      this.AddProject();
    }
    else {
      this.AddProject();
    }
  }

  AddProject() {
    this.object = new ProjectModel();
    this.object.Project = this.Project;
    this.object.Priority = this.Priority;
    if (this.StartEndDateSelected) {
      this.object.StartDate = new Date(this.StartDate);
      this.object.EndDate = new Date(this.EndDate);
    }
    this.object.ManagerID = this.UserID;

    this.apiService.AddProject(this.object)
      .subscribe((data: any) => {
        console.log(data);
        this.ResetData();
        this.GetProjects();
      },
        function (error) {
          console.log(error);
        });
  }

  openDialog() {
    let disposable = this.dialogService.addDialog(UserListModelComponent, this.UserList)
      .subscribe((selectedUser) => {
        if (selectedUser) {
          this.UserID = selectedUser.UserID;
          this.ManagerName = selectedUser.FirstName + ' ' + selectedUser.LastName;
        }
        else {
          alert('declined');
        }
      });
    setTimeout(() => {
      disposable.unsubscribe();
    }, 10000);
  }

  GetUsers() {
    this.apiService.GetUsers()
      .subscribe((data: UserModel[]) => {
        console.log(data);
        this.UserList = data;
      },
        function (error) {
          console.log(error);
        });
  }

  assignCopy() {
    this.filteredList = Object.assign([], this.projectList);
    this.projListCount = this.filteredList.length;
  }

  ResetData() {
    this.object = new ProjectModel();
    this.Project = undefined;
    this.Priority = undefined;
    this.StartEndDateSelected = false;
    this.StartDate = undefined;
    this.EndDate = undefined;
    this.UserID = undefined;
    this.AddButtonText = "Add";
    this.ResetButtonText = "Reset";
  }

  DateCheckBoxChange() {
    if (this.StartEndDateSelected) {
      this.StartDate = new Date().toISOString().split('T')[0];;
      let tmpDate = new Date();
      tmpDate.setDate(tmpDate.getDate() + 1);
      this.EndDate = tmpDate.toISOString().split('T')[0];
    }
    else {
      this.StartDate = undefined;
      this.EndDate = undefined;
    }
  }

}
