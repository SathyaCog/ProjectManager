import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { RouterModule, Routes } from '@angular/router'
import { HttpModule } from '@angular/http'
import { AppComponent } from './app.component';
import { ProjectsComponent } from './projects/projects.component';
import { UsersComponent } from './users/users.component';
import { AddTasksComponent } from './add-tasks/add-tasks.component';
import { ViewTasksComponent } from './view-tasks/view-tasks.component';
import { appRoutes } from './app-routing.module';
import { ApiService } from './service/api-service';
import { BootstrapModalModule } from 'ng2-bootstrap-modal';
import { UserListModelComponent } from './user-list-model/user-list-model.component';

@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    UsersComponent,
    AddTasksComponent,
    ViewTasksComponent,
    UserListModelComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    BootstrapModalModule,
    RouterModule.forRoot(appRoutes),
    BootstrapModalModule.forRoot({ container: document.body })
  ],
  entryComponents: [
    UserListModelComponent
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
