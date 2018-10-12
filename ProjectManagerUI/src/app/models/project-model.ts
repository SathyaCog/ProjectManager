export class ProjectModel {
    ProjectID: number;
    Project: string;
    StartDate?: Date;
    EndDate?: Date;
    Priority: number;
    ManagerID: number;
    NoofTasks: number;
    NoofCompletedTasks: number;
}
