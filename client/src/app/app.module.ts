import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DatePickerComponent } from './date-picker/date-picker.component';
import { DailyLogComponent } from './daily-log/daily-log.component';
import { RouterOutlet } from '@angular/router';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { TodoitemsComponent } from './todoitems/todoitems.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TokenInterceptorInterceptor } from './interceptor/token-interceptor.interceptor';
import { TrackedTodoitemsComponent } from './tracked-todoitems/tracked-todoitems.component';
import { TaskDeleteModal, UsertoditemTableComponent } from './components/usertoditem-table/usertoditem-table.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AddtaskmodalComponent } from './components/modals/addtaskmodal/addtaskmodal.component';
import { ErrorInterceptor } from './interceptor/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { HomeComponent } from './home/home.component';
@NgModule({
  declarations: [
    AppComponent,
    DatePickerComponent,
    DailyLogComponent,
    TodoitemsComponent,
    LoginComponent,
    RegisterComponent,
    NavBarComponent,
    TextInputComponent,
    TrackedTodoitemsComponent,
    UsertoditemTableComponent,
    TaskDeleteModal,
    AddtaskmodalComponent,
    TestErrorComponent,
    NotfoundComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    BsDropdownModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-top-right',
      preventDuplicates:true
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
