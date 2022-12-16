import { createAction, props } from '@ngrx/store';
import { AccountInfo } from '../accountInfo.model';

export const createAccountStart = createAction(
  '[From Account] Create Account Start',
  props<{
    customerId: number;
    initialCredits: number;
  }>()
);
export const getAccountInfoStart = createAction(
  '[From Account] Get Account Info',
  props<{
    customerId: number;
    accountId: number;
  }>()
);
export const getAccountInfoSuccess = createAction(
  '[From Account] Get Account Info Success',
  props<{
    accountInfo: AccountInfo;
  }>()
);
export const getAccountInfoError = createAction(
  '[From Account] Get Account Info Error',
  props<{
    message: string;
  }>()
);
export const createAccountSuccess = createAction(
  '[From Account] Create Account Success',
  props<{
    message: string;
  }>()
);
export const createAccountFail = createAction(
  '[From Account] Create Account Fail',
  props<{
    message: string;
  }>()
);

export const clearError = createAction('[Auth] Clear Error');
