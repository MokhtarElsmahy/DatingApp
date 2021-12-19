import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private route :Router , private toastr :ToastrService) {}

  //request: HttpRequest<unknown> : to intercept requests  
  //next: HttpHandler : to intercept the response 
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error =>{
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateError = [];
                for (const key in error.error.errors) {
                  modalStateError.push(error.error.errors[key])
                }
                throw modalStateError.flat(); // add es2019 in tsconfig.json and re-run project
              } else {
                this.toastr.error("Bad Request", error.status);
              }
              break;
            case 401:
              this.toastr.error("UnAuthorized", error.status);

              break;
            case 404:
              this.route.navigateByUrl('/not-found');

              break;

              case 500:
              const  navigationExtras :NavigationExtras = {state:{error:error.error}} 
                this.route.navigateByUrl('/server-error',navigationExtras);
  
                break;
            default:
              this.toastr.error("something unexpected went wrong");
              console.log(error)
              break;

           
          }
        }
        return throwError(error)
      })
        
    );
  }
}
