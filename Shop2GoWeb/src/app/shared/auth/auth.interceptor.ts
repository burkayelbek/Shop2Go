import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { tap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{
 
    constructor(private _router:Router) {
        
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        var token = localStorage.getItem('token');
        if(token != null){
            const clonedRequest = req.clone({
                headers: req.headers.set('Authorization','Bearer '+ token)
            });
            return next.handle(clonedRequest).pipe(
                tap(
                    success => { },
                    err => {
                        if(err.status == 401){
                            localStorage.removeItem('token');
                            this._router.navigateByUrl('/Login');
                        }
                        else if(err.status== 403){
                            this._router.navigateByUrl('/forbidden');
                        }
                    }
                )
            )
        }
        else
            return next.handle(req.clone());
    }
}