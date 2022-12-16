import { state } from '@angular/animations';
import { Action, createReducer, on } from '@ngrx/store';
import { Customer } from '../../shared/Customer.Model';
import * as AuthActions from './auth.actions';

export interface State {
  customer: Customer;
  errorMessage: string;
  loading: boolean;
}

const initialState: State = {
  customer: null,
  errorMessage: null,
  loading: false,
};

const _authReducer = createReducer(
  initialState,

  on(AuthActions.loginStart, AuthActions.signupStart, (state) => ({
    ...state,
    errorMessage: null,
    loading: true,
  })),
  on(AuthActions.authenticateSuccess, (state, action) => ({
    ...state,
    errorMessage: null,
    loading: false,
    customer: new Customer(
      action.customerId,
      action.firstName,
      action.lastName,
      action.email,
      action.token,
      action.expirationDate
    ),
  })),

  on(AuthActions.authenticateFail, (state, action) => ({
    ...state,
    errorMessage: action.errorMessage,
    loading: false,
  })),
  on(AuthActions.autoLogin, (state) => ({
    ...state,
  })),
  on(AuthActions.logout, (state) => ({
    ...state,
    customer: null,
  }))
);
export function authReducer(state: State, action: Action) {
  return _authReducer(state, action);
}
