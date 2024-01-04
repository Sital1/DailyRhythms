import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DailyLogComponent } from './daily-log/daily-log.component';

const routes: Routes = [
  {path:'DailyLog/:date/:userId',component:DailyLogComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
