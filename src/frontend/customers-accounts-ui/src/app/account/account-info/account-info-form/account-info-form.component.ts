import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Store } from '@ngrx/store';
import { map, take } from 'rxjs';
import * as fromApp from '../../../store/app.reducer';
import * as AccountActions from '../../store/account.actions';

@Component({
  selector: 'app-account-info-form',
  templateUrl: './account-info-form.component.html',
  styleUrls: ['./account-info-form.component.css'],
})
export class AccountInfoFormComponent implements OnInit, OnDestroy {
  constructor(private store: Store<fromApp.AppState>) {}

  ngOnInit(): void {}
  ngOnDestroy(): void {}
  onCreate(form: NgForm) {
    if (!form.valid) {
      return;
    }
    this.store
      .select('auth')
      .pipe(
        take(1),
        map((authState) => {
          return authState.customer;
        })
      )
      .subscribe({
        next: (customer) => {
          this.store.dispatch(
            AccountActions.getAccountInfoStart({
              customerId: customer.customerId,
              accountId: form.value.accountId,
            })
          );
        },
      });
  }
}
