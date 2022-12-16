import { Action, createReducer, on } from '@ngrx/store';
import { AccountInfo } from '../accountInfo.model';
import * as AccountActions from './account.actions';

export interface State {
  message: string;
  loading: boolean;
  accountInfo: AccountInfo;
}
const initialState: State = {
  message: null,
  loading: false,
  accountInfo: null,
};

const _accountReducer = createReducer(
  initialState,

  on(
    AccountActions.createAccountStart,
    AccountActions.getAccountInfoStart,
    (state) => ({
      ...state,
      message: null,
      loading: true,
      accountInfo: null,
    })
  ),
  on(
    AccountActions.createAccountSuccess,
    AccountActions.createAccountFail,
    (state, action) => ({
      ...state,
      message: action.message,
      loading: false,
    })
  ),

  on(AccountActions.getAccountInfoError, (state, actions) => ({
    ...state,
    message: actions.message,
    loading: false,
  })),
  on(AccountActions.getAccountInfoSuccess, (state, actions) => ({
    ...state,
    message: null,
    loading: false,
    accountInfo: actions.accountInfo,
  })),
  on(AccountActions.clearError, (state) => ({
    ...state,
    message: null,
  }))
);

export function accountReducer(state: State, action: Action) {
  return _accountReducer(state, action);
}
