import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AlertComponent } from 'src/app/shared/alert/alert.component';
import { PlaceholderDirective } from 'src/app/shared/placeholder.directive';
import * as fromApp from '../../store/app.reducer';
import { AccountInfo } from '../accountInfo.model';
import * as AccountActions from '../store/account.actions';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css'],
})
export class AccountInfoComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  constructor(private store: Store<fromApp.AppState>) {}

  storeSub: Subscription;
  @ViewChild(PlaceholderDirective, { static: false })
  placeholder: PlaceholderDirective;
  message: string = null;
  accountInfo: AccountInfo = null;
  ngOnInit(): void {
    this.storeSub = this.store.select('account').subscribe({
      next: (accountState) => {
        this.message = accountState.message;
        this.isLoading = accountState.loading;
        this.accountInfo = accountState.accountInfo;
        if (this.message) {
          this.showAlert(this.message);
        }
      },
    });
  }

  ngOnDestroy(): void {
    if (this.storeSub) {
      this.storeSub.unsubscribe();
    }
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
}
