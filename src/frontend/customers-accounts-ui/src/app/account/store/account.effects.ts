import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { AccountInfo } from '../accountInfo.model';
import * as AccountActions from './account.actions';

const handleError = (errorRes) => {
  let errorMessage = 'an unknown error has occured';
  if (!errorRes.error || !errorRes.error.Message || errorRes.status == 500) {
    return of(AccountActions.createAccountFail({ message: errorMessage }));
  }
  if (errorRes.error.Message) {
    errorMessage = errorRes.error.Message;
  }
  return of(AccountActions.createAccountFail({ message: errorMessage }));
};

@Injectable()
export class AccountEffects {
  createAccount$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AccountActions.createAccountStart),
      switchMap((action) => {
        return this.http
          .post(
            'https://localhost:7112/api/customers/' +
              action.customerId +
              '/accounts?initialCredits=' +
              action.initialCredits,

            {}
          )
          .pipe(
            map(() => {
              const message = 'Account created successfully';
              return AccountActions.createAccountSuccess({
                message: message,
              });
            }),

            catchError((erroRes) => {
              return handleError(erroRes);
            })
          );
      })
    )
  );

  getAccountInfo$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AccountActions.getAccountInfoStart),
      switchMap((actions) => {
        return this.http
          .get(
            'https://localhost:7112/api/customers/' +
              actions.customerId +
              '/accounts/' +
              actions.accountId +
              '/userInfo'
          )
          .pipe(
            map((response: AccountInfo) => {
              return AccountActions.getAccountInfoSuccess({
                accountInfo: new AccountInfo(
                  response.firstName,
                  response.surname,
                  response.balance,
                  response.transactions
                ),
              });
            }),
            catchError((errorRes) => {
              return handleError(errorRes);
            })
          );
      })
    )
  );

  constructor(private actions$: Actions, private http: HttpClient) {}
}
