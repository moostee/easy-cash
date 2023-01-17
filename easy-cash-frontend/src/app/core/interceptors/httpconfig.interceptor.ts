import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { TokenStorageService } from "../services/token-storage.service";

const TOKEN_HEADER_KEY = 'Authorization';

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {

    constructor(private token: TokenStorageService, private _snackBar: MatSnackBar) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (!request.headers.has('Content-Type')) {
            request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
        }

        request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
        request = request.clone({ headers: request.headers.set('Access-Control-Allow-Origin', '*') });

        // Add token to requests
        const token = this.token.getToken();
        if (token != null) {
            request = request.clone({ headers: request.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + token) });
        }

        return next.handle(request).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    // console.log('event--->>>', event);
                }
                return event;
            }),
            catchError((error: HttpErrorResponse) => {
                console.log('error--->>>', error);

                let data = {};
                data = {
                    reason: error && error.error && error.error.reason ? error.error.reason : '',
                    status: error.status
                };

                this._snackBar.open(error && error.error && error.error.Message ? error.error.Message : 'Service is unavailable at the moment. Please try again later', 'Ok', {
                    duration: 5000
                })

                return throwError(error);
            }));
    }

}