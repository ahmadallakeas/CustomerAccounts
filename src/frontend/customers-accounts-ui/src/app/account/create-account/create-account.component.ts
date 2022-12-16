import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Store } from '@ngrx/store';
import { map, Subscription, take } from 'rxjs';
import { AlertComponent } from 'src/app/shared/alert/alert.component';
import { PlaceholderDirective } from 'src/app/shared/placeholder.directive';
import * as fromApp from '../../store/app.reducer';
import * as AccountActions from '../store/account.actions';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css'],
})
export class CreateAccountComponent implements OnInit, OnDestroy {
  constructor(private store: Store<fromApp.AppState>) {}
  storeSub: Subscription;
  @ViewChild(PlaceholderDirective, { static: false })
  placeholder: PlaceholderDirective;
  isLoading = false;
  message: string = null;

  ngOnInit(): void {
    this.storeSub = this.store.select('account').subscribe({
      next: (accountState) => {
        this.message = accountState.message;
        this.isLoading = accountState.loading;
        if (this.message) {
          this.showAlert(this.message);
        }
      },
    });
  }

  onCreate(form: NgForm) {
    this.store
      .select('auth')
      .pipe(
        take(1),
        map((authState) => {
          return authState.customer;
        })
      )
      .subscribe((customer) => {
        this.store.dispatch(
          AccountActions.createAccountStart({
            customerId: customer.customerId,
            initialCredits: form.value.credit,
          })
        );
      });
    form.reset();
  }

  private showAlert(error: string) {
    const containerRef = this.placeholder.viewContainerRef;
    containerRef.clear();
    const component = containerRef.createComponent(AlertComponent);
    component.instance.message = error;
    const event = component.instance.closed.subscribe(() => {
      this.store.dispatch(AccountActions.clearError());
      event.unsubscribe();
      containerRef.clear();
    });
  }

  ngOnDestroy(): void {
    if (this.storeSub) {
      this.storeSub.unsubscribe();
    }
  }
}
