import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { tap } from 'rxjs';
import { ITasksList } from 'src/app/models/TasksList';
import { TasksService } from 'src/app/services/tasks.service';
import { getUniqueId } from 'src/app/utility/utility';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css'],
})
export class TodoListComponent implements OnInit {
  constructor(private tasksService: TasksService) {}

  ngOnInit(): void {
    this.tasksService.getAllTaskList().subscribe((list) => {
      this.List = list;
    });
  }

  taskListId: number = 0;
  taskListTitle: string = '';

  newList: ITasksList = {
    id: 0,
    title: '',
  };

  formList = new FormGroup({
    title: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(2),
    ]),
  });
  get title() {
    return this.formList.controls.title as FormControl;
  }

  List: ITasksList[] = [];

  removelistT(id: number) {
    this.tasksService
      .removeValue<ITasksList>(id, 'ListTasks?key=')
      .pipe(tap(() => this.List = this.List.filter((item) => item.id !== id)))
      .subscribe();
  }

  addlistTask() {
    if (this.newList != null) {
      this.newList.id = getUniqueId();
      this.newList.title = this.formList.controls.title.value as string;
      this.tasksService
        .addValue<ITasksList>(this.newList, 'ListTasks')
        .pipe(tap(() => this.List.push(this.newList)))
        .subscribe();
      this.formList.controls.title.setValue('');
    }
  }
}
