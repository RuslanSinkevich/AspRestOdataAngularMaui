import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ITasks } from '../models/Tasks';
import { Observable, catchError, delay, retry, throwError } from 'rxjs';
import { ErrorService } from './error.service';
import { ITasksList } from '../models/TasksList';
import { API_URL } from '../utility/constants';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  constructor(private http: HttpClient, private errorService: ErrorService) {}

  path: string = API_URL;

  // Tasks
  getAllTasks(idList: number): Observable<ITasks[]> {
    return this.http
      .get<ITasks[]>(this.path + 'Tasks?$filter=taskListId eq ' + idList)
      .pipe(catchError(this.handleError.bind(this)));
  }

  getAllTasksStatusId(idList: number, statusId: number): Observable<ITasks[]> {
    return this.http
      .get<ITasks[]>(
        this.path +
          'Tasks?$filter=taskListId eq ' +
          idList +
          ' and statusId eq ' +
          statusId
      )
      .pipe(catchError(this.handleError.bind(this)));
  }

  // TasksList
  getAllTaskList() {
    return this.http
      .get<ITasksList[]>(this.path + 'ListTasks')
      .pipe(catchError(this.handleError.bind(this)));
  }


  // Generic type
  addValue<T>(Value: T, url: string ){
    return this.http
      .post(this.path + url, Value)
      .pipe(catchError(this.handleError.bind(this)));
  }

  editValue<T>(Value: T, url: string ){
    return this.http
      .put(this.path + url, Value)
      .pipe(catchError(this.handleError.bind(this)));
  }

  removeValue<T>(idValue: number, url: string )  {
    return this.http
      .delete(this.path +  url + idValue )
      .pipe(catchError(this.handleError.bind(this)));
  }

  private handleError(error: HttpErrorResponse) {
    this.errorService.handle(error.message);
    return throwError(() => error.message);
  }
}
