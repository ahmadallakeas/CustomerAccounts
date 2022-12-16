import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, last, map, of, switchMap, tap } from 'rxjs';
import { Customer } from 'src/app/auth/customer.Model';
import { AuthService } from '../auth.service';
import * as AuthActions from './auth.actions';

export interface AuthResponseData {
  customerId: number;
  firstName: string;
  lastName: string;
  email: string;
  token: string;
  expiresIn: number;
}

const handleAuthentication = (
  customerId: number,
  firstName: string,
  lastName: string,
  email: string,
  token: string,
  expiresIn: number
) => {
  const expirationDate = new Date(new Date().getTime() + expiresIn * 1000);
  const user = new Customer(
    customerId,
    firstName,
    lastName,
    email,
    token,
    expirationDate
  );
  localStorage.setItem('customer', JSON.stringify(user));
  return AuthActions.authenticateSuccess({
    customerId,
    firstName,
    lastName,
    email,
    token,
    expirationDate,
    redirect: true,
  });
};

const handleError = (errorRes: any) => {
  let errorMessage = 'An unknown error occurred!';
  if (
    !errorRes.error ||
    errorRes.statusText === 'Unknown Error' ||
    errorRes.status == 500
  ) {
    return of(AuthActions.authenticateFail({ errorMessage }));
  }
  if (errorRes.error.Message) {
    errorMessage = errorRes.error.Message;
  } else {
    errorMessage = 'Wrong email or password';
  }

  return of(AuthActions.authenticateFail({ errorMessage }));
};
const handleRegisterError = (errorRes: any) => {
  let errorMessage = 'An unknown error occurred!';
  if (
    !errorRes.error ||
    errorRes.statusText === 'Unknown Error' ||
    errorRes.status == 500
  ) {
    return of(AuthActions.authenticateFail({ errorMessage }));
  }
  var errors = Object.values(errorRes.error);
  errorMessage = errors[0][0];
  return of(AuthActions.authenticateFail({ errorMessage }));
};

@Injectable()
export class AuthEffects {
  authLogin$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.loginStart),
      switchMap((action) => {
        return this.http
          .post('https://localhost:7112/api/authenticate/login', {
            Email: action.email,
            Password: action.password,
          })
          .pipe(
            map((response: AuthResponseData) => {
              return handleAuthentication(
                response.customerId,
                response.firstName,
                response.lastName,
                response.email,
                response.token,
                response.expiresIn
              );
            }),
            catchError((errorRes) => {
              return handleError(errorRes);
            })
          );
      })
    )
  );
  authRegister$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.signupStart),
      switchMap((action) => {
        return this.http
          .post('https://localhost:7112/api/authenticate/register', {
            FirstName: action.firstName,
            LastName: action.lastName,
            Email: action.email,
            Password: action.password,
          })
          .pipe(
            map((response: AuthResponseData) => {
              return handleAuthentication(
                response.customerId,
                response.firstName,
                response.lastName,
                response.email,
                response.token,
                response.expiresIn
              );
            }),
            catchError((errorRes) => {
              return handleRegisterError(errorRes);
            })
          );
      })
    )
  );
  authRedirect$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.authenticateSuccess),
        tap((action) => action.redirect && this.router.navigate(['/']))
      ),
    { dispatch: false }
  );
  authLogout$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.logout),
        tap(() => {
          this.authService.clearLogoutTimer();
          localStorage.removeItem('customer');
          this.router.navigate(['/login']);
        })
      ),
    { dispatch: false }
  );
  authAutoLogin$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.autoLogin),
      map(() => {
        const customer: {
          customerId: number;
          email: string;
          firstName: string;
          lastName: string;
          _token: string;
          _tokenExpirationDate: string;
        } = JSON.parse(localStorage.getItem('customer'));
        if (!customer) {
          return { type: 'DUMMY' };
        }
        const loadedCustomer = new Customer(
          customer.customerId,
          customer.firstName,
          customer.lastName,
          customer.email,
          customer._token,
          new Date(customer._tokenExpirationDate)
        );

        if (loadedCustomer) {
          // this.user.next(loadedUser);
          const expirationDuration =
            new Date(customer._tokenExpirationDate).getTime() -
            new Date().getTime();
          this.authService.setLogoutTimer(expirationDuration);
          return AuthActions.authenticateSuccess({
            firstName: loadedCustomer.firstName,
            lastName: loadedCustomer.lastName,
            email: loadedCustomer.email,
            customerId: loadedCustomer.customerId,
            token: loadedCustomer.token,
            expirationDate: new Date(customer._tokenExpirationDate),
            redirect: false,
          });
        }
        return { type: 'DUMMY' };
      })
    )
  );
  constructor(
    private actions$: Actions,
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) {}
}
