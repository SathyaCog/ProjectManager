import { Component, OnInit } from '@angular/core';
import { UserModel } from '../models/user-model';
import { ApiService } from '../service/api-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  FirstName: string;
  LastName: string;
  EmployeeID: string;
  UserID: number;
  AddButtonText: string;
  ResetButtonText: string;
  object: UserModel;
  UserList: UserModel[];
  filteredList: UserModel[];
  searchText: string;
  sortByFName: string;
  userListCount: number;

  constructor(private apiService: ApiService) {

  }

  ngOnInit() {
    this.AddButtonText = "Add User";
    this.ResetButtonText = "Reset";
    this.GetUsers();
  }

  GetUsers() {
    this.apiService.GetUsers()
      .subscribe((data: UserModel[]) => {
        console.log(data);
        this.UserList = data;
        this.assignCopy();
      },
        function (error) {
          console.log(error);
        });
  }

  AddUpdateUser() {
    if (this.UserID) {
      this.UpdateUser();
    }
    else {
      this.AddUser();
    }
  }

  AddUser() {
    this.object = new UserModel();
    this.object.FirstName = this.FirstName;
    this.object.LastName = this.LastName;
    this.object.EmployeeID = this.EmployeeID;

    this.apiService.AddUser(this.object)
      .subscribe((data: any) => {
        console.log(data);
        this.ResetData();
        this.GetUsers();
      },
        function (error) {
          console.log(error);
        });
  }

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }

  assignCopy() {
    this.filteredList = Object.assign([], this.UserList);
    this.userListCount = this.filteredList.length;
  }

  filterItem() {
    if (!this.searchText) this.assignCopy();
    this.filteredList = Object.assign([], this.UserList).filter(
      item => (item.FirstName != undefined ? item.FirstName.toLowerCase().indexOf(this.searchText.toLowerCase()) > -1 : true) ||
        (item.LastName != undefined ? item.LastName.toLowerCase().indexOf(this.searchText.toLowerCase()) > -1 : true) ||
        (item.EmployeeID != undefined ? item.EmployeeID.toLowerCase().indexOf(this.searchText.toLowerCase()) > -1 : true)
    )
  }

  UpdateUser() {
    this.object = new UserModel();
    this.object.UserID = this.UserID;
    this.object.FirstName = this.FirstName;
    this.object.LastName = this.LastName;
    this.object.EmployeeID = this.EmployeeID;

    this.apiService.UpdateUser(this.object)
      .subscribe((data: any) => {
        console.log(data);
        this.ResetData()
        this.GetUsers();
      },
        function (error) {
          console.log(error);
        });
  }

  DeleteUser(user) {
    let obj = new UserModel();
    obj.UserID = user.UserID;
    obj.FirstName = user.FirstName;
    obj.LastName = user.LastName;
    obj.EmployeeID = user.EmployeeID;

    this.apiService.DeleteUser(obj)
      .subscribe((data: any) => {
        console.log(data);
        this.ResetData();
        this.GetUsers();
      },
        function (error) {
          console.log(error);
        });
  }

  EditUser(user) {
    this.AddButtonText = "Update";
    this.ResetButtonText = "Cancel";
    this.FirstName = user.FirstName;
    this.LastName = user.LastName;
    this.EmployeeID = user.EmployeeID;
    this.UserID = user.UserID;
  }

  ResetData() {
    this.object = new UserModel();
    this.FirstName = undefined;
    this.LastName = undefined;
    this.EmployeeID = undefined;
    this.UserID = undefined;
    this.AddButtonText = "Add User";
    this.ResetButtonText = "Reset";
  }

  sortingUser(sort) {
    if (sort == 'FName') {
      this.filteredList.sort((a, b) => {
        if (a.FirstName < b.FirstName) return -1;
        else if (a.FirstName > b.FirstName) return 1;
        else return 0;
      });
    }
    else if (sort == 'LName') {
      this.filteredList.sort((a, b) => {
        if (a.LastName < b.LastName) return -1;
        else if (a.LastName > b.LastName) return 1;
        else return 0;
      });
    }
    else if (sort == 'EId') {
      this.filteredList.sort((a, b) => {
        if (a.EmployeeID < b.EmployeeID) return -1;
        else if (a.EmployeeID > b.EmployeeID) return 1;
        else return 0;
      });
    }

  }

}
