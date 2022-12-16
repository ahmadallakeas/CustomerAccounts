import { ActionReducerMap } from '@ngrx/store';
import * as fromAuth from '../auth/store/auth.reducer';
import * as fromAccount from '../account/store/account.reducer';
export interface AppState {
  auth: fromAuth.State;
  account: fromAccount.State;
}

export const appReducer: ActionReducerMap<AppState> = {
  auth: fromAuth.authReducer,
  account: fromAccount.accountReducer,
};
