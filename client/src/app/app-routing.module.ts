import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DailyLogComponent } from './daily-log/daily-log.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { DatePickerComponent } from './date-picker/date-picker.component';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: 'DailyLog/:date/:userId', component: DailyLogComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'viewlogs', canActivate:[authGuard], component: DatePickerComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
