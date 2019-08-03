import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { Storage } from '@ionic/storage';
import { AuthService } from './auth.service';
import { mergeMap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private storage: Storage, private authService: AuthService) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    return from(this.authService.getToken()).pipe(mergeMap((token) => {
      //console.log("token", token);
      if (!token) {
        return next.handle(req);
      }

      const changedReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });

      return next.handle(changedReq);
    }));

    // this.storage.get("ACCESS_TOKEN").then(t => {


    //   const req1 = req.clone({
    //     headers: req.headers.set('Authorization', `Bearer ${t}`),
    //   });

    //   return next.handle(req1);
    // }); // you probably want to store it in localStorage or something


  }

}