import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { exhaustMap, map, Observable, switchMap, take } from 'rxjs';

import * as fromApp from '../store/app.reducer';

@Injectable({
  providedIn: 'root',
})
export class AccountInterceptorService implements HttpInterceptor {
  constructor(private store: Store<fromApp.AppState>) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return this.store.select('auth').pipe(
      take(1),
      map((authState) => {
        return authState.customer;
      }),
      exhaustMap((customer) => {
        if (!customer) {
          return next.handle(req);
        }
        const request = req.clone({
          setHeaders: {
            Authorization: `Bearer ${customer.token}`,
          },
        });
        return next.handle(request);
      })
    );
  }
}
