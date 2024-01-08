import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DailyLogComponent } from './daily-log/daily-log.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { DatePickerComponent } from './date-picker/date-picker.component';
import { authGuard } from './guards/auth.guard';
import { TrackedTodoitemsComponent } from './tracked-todoitems/tracked-todoitems.component';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotfoundComponent } from './notfound/notfound.component';

const routes: Routes = [
  { path: 'DailyLog/:date/:userId',canActivate:[authGuard], component: DailyLogComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'viewlogs', component: DatePickerComponent },
  { path: 'trackedToDoItems', canActivate:[authGuard], component: TrackedTodoitemsComponent },
  { path: 'test-error', component: TestErrorComponent },
  { path: 'not-found', component: NotfoundComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
