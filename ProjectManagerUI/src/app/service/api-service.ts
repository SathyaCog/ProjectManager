import { Injectable } from '@angular/core';
import { Http, HttpModule, Response, Headers, RequestOptions } from '@angular/http'
import 'rxjs/Rx'
import { Observable } from 'rxjs';
import { UserModel } from '../models/user-model';
import { ProjectModel } from '../models/project-model';

import { map } from 'rxjs/operators';
import 'rxjs/add/operator/map';

@Injectable()
export class ApiService {
    constructor(private _http: Http) {

    }
    userServiceUrl: string = "http://localhost:50329/api/Users";
    projectServiceUrl: string = "http://localhost:50329/api/Projects";
    taskServiceUrl: string = "http://localhost:50329/api/Tasks";

    GetUsers(): Observable<UserModel[]> {
        let getUrl = this.userServiceUrl + '/GetUsers';
        return this._http.get(getUrl)
            .pipe(map(response => { return response.json() }))
            .catch(this.handleError)
    }

    AddUser(user) {
        let body = JSON.stringify(user);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let addUrl = this.userServiceUrl + '/AddUser'

        return this._http.post(addUrl, body, options)
            .catch(this.handleError);
    }

    UpdateUser(user) {
        let body = JSON.stringify(user);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let updateUrl = this.userServiceUrl + '/UpdateUser'

        return this._http.post(updateUrl, body, options)
            .catch(this.handleError);
    }

    DeleteUser(user) {
        let body = JSON.stringify(user);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let updateUrl = this.userServiceUrl + '/DeleteUser'

        return this._http.post(updateUrl, body, options)
            .catch(this.handleError);
    }

    GetProjects(): Observable<ProjectModel[]> {
        let getUrl = this.projectServiceUrl + '/GetProjects';
        return this._http.get(getUrl)
            .pipe(map(response => { return response.json() }))
            .catch(this.handleError)
    }

    AddProject(project) {
        let body = JSON.stringify(project);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let addUrl = this.projectServiceUrl + '/AddProject'

        return this._http.post(addUrl, body, options)
            .catch(this.handleError);
    }

    UpdateProject(project) {
        let body = JSON.stringify(project);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let updateUrl = this.projectServiceUrl + '/UpdateProject'

        return this._http.post(updateUrl, body, options)
            .catch(this.handleError);
    }

    SuspendProject(projectID) {
        let body = JSON.stringify(projectID);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let deleteUrl = this.projectServiceUrl + '/SuspendProject'

        return this._http.post(deleteUrl, body, options)
            .catch(this.handleError);
    }

    AddTask(task) {
        let body = JSON.stringify(task);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let addUrl = this.taskServiceUrl + '/AddTask'

        return this._http.post(addUrl, body, options)
            .catch(this.handleError);
    }

    AddParentTask(task) {
        let body = JSON.stringify(task);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let addUrl = this.taskServiceUrl + '/AddParentTask'

        return this._http.post(addUrl, body, options)
            .catch(this.handleError);
    }

    private handleError(error: Response | any) {
        console.error('UserService::handleError', error);
        return Observable.throw(error);
    }
}

