import { Component, Input, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AccountInfo } from '../../accountInfo.model';
import * as fromApp from '../../../store/app.reducer';
import * as AuthActions from '../../store/account.actions';

@Component({
  selector: 'app-account-info-details',
  templateUrl: './account-info-details.component.html',
  styleUrls: ['./account-info-details.component.css'],
})
export class AccountInfoDetailsComponent implements OnDestroy {
  @Input() accountInfo: AccountInfo;

  constructor(private store: Store<fromApp.AppState>) {}
  ngOnDestroy(): void {
    this.store.dispatch(AuthActions.resetAccountInfo());
  }
}
