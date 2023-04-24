import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { GlobalErrorComponent } from './components/global-error/global-error.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TodoTasksComponent } from './components/todo-list/todo-tasks/todo-tasks.component';
import { TodoListComponent } from './components/todo-list/todo-list.component';

@NgModule({
  declarations: [AppComponent, GlobalErrorComponent, TodoTasksComponent, TodoListComponent],
  imports: [BrowserModule, HttpClientModule, FormsModule, ReactiveFormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
