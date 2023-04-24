import { Component, Input, OnInit } from '@angular/core';
import { ITasks } from 'src/app/models/Tasks';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TasksService } from 'src/app/services/tasks.service';
import { getUniqueId } from 'src/app/utility/utility';
import { tap } from 'rxjs';

@Component({
  selector: 'app-todo-tasks',
  templateUrl: './todo-tasks.component.html',
  styleUrls: ['./todo-tasks.component.scss'],
})
export class TodoTasksComponent implements OnInit {
  constructor(private tasksService: TasksService) {}

  ngOnInit(): void {
    this.tasksService.getAllTasks(0).subscribe((task) => {
      this.Tasks = task;
    });
  }

  @Input() taskListId!: number;
  @Input() taskListTitle!: string;

  newTask: ITasks = {
    id: 0,
    title: '',
    statusId: 1,
    taskListId: 2,
  };

  form = new FormGroup({
    title: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(2),
    ]),
  });

  get title() {
    return this.form.controls.title as FormControl;
  }

  statusId: number = 1;

  Tasks: ITasks[] = [];

  ngOnChanges() {
    this.task();
  }

  task() {
    if (this.statusId == 0) {
      this.tasksService.getAllTasks(this.taskListId).subscribe((task) => {
        this.Tasks = task;
      });
    } else {
      this.tasksService
        .getAllTasksStatusId(this.taskListId, this.statusId)
        .subscribe((task) => {
          this.Tasks = task;
        });
    }
  }

  addTask() {
    if (this.newTask != null) {
      this.newTask.id = getUniqueId();
      this.newTask.title = this.form.controls.title.value as string;
      this.newTask.statusId = 1;
      this.newTask.taskListId = this.taskListId;
      this.tasksService
        .addValue<ITasks>(this.newTask, 'Tasks')
        .pipe(tap(() => this.Tasks.push(this.newTask)))
        .subscribe();
      this.form.controls.title.setValue('');
    }
  }

  // Если задача помечена как удалённая, при повторном удалении полностью стираем задачу.
  editTask(tasks: ITasks, statusId: number) {
    if (tasks.statusId === 3 && statusId === 3) {
      this.tasksService
        .removeValue<ITasks>(tasks.id, 'Tasks?key=')
        .pipe(
          tap(
            () =>
              (this.Tasks = this.Tasks.filter((item) => item.id !== tasks.id))
          )
        )
        .subscribe();
      console.log(tasks);
    } else {
      tasks.statusId = statusId;
      this.tasksService
        .editValue<ITasks>(tasks, 'Tasks?key=' + tasks.id)
        .pipe(
          tap(
            () =>
              (this.Tasks[this.Tasks.findIndex(u=> u.id === tasks.id)].statusId=statusId)
          )
        )
        .subscribe();
    }
  }
}
