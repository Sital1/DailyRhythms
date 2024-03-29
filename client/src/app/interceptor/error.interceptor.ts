import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  matcheDailyLogUrl(url: string): boolean {

    const pattern = /api\/DailyLogs\/\d+\/\d{4}-\d{2}-\d{2}/;
    return pattern.test(url);

  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          if (error.status == 401) {
            this.toastr.error(error.error.message, error.status.toString());
          }
          if (error.status == 400) {
            if (error.error.errors) {
              throw error.error;
            }
            this.toastr.error(error.error.message, error.status.toString());
          }
          if (error.status == 404) {
            if (this.matcheDailyLogUrl(request.url.toString())){

              throw error.error;
            }
              this.router.navigateByUrl('/not-found');
          }
          // if (error.status == 500) {
          //   const navigationExtras: NavigationExtras = { state: { error: error.error } }
          //   this.router.navigateByUrl('/server-error', navigationExtras);
          // }

        }
        return throwError(() => new Error(error.message))
      })
    )
  }
}

