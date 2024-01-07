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
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TokenInterceptorInterceptor } from './interceptor/token-interceptor.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    DatePickerComponent,
    DailyLogComponent,
    TodoitemsComponent,
    LoginComponent,
    RegisterComponent,
    NavBarComponent,
    TextInputComponent
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
    BsDropdownModule
  ],
  providers: [{provide:HTTP_INTERCEPTORS, useClass:TokenInterceptorInterceptor,multi:true},],
  bootstrap: [AppComponent]
})
export class AppModule { }
