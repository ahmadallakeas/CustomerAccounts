import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { OnInit, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import * as AuthActions from '../store/auth.actions';
import * as fromApp from '../../store/app.reducer';
import { PlaceholderDirective } from 'src/app/shared/placeholder.directive';
import { AlertComponent } from 'src/app/shared/alert/alert.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild(PlaceholderDirective, { static: false })
  placeholder: PlaceholderDirective;
  storeSub: Subscription;
  isLoading = false;
  errorMessage: string = null;

  constructor(private store: Store<fromApp.AppState>) {}

  ngOnInit(): void {
    this.storeSub = this.store.select('auth').subscribe({
      next: (authState) => {
        this.errorMessage = authState.errorMessage;
        this.isLoading = authState.loading;
        if (this.errorMessage) {
          this.showErrorAlert(this.errorMessage);
        }
      },
    });
  }

  onSubmit(loginForm: NgForm) {
    if (!loginForm.valid) {
      return;
    }
    const email = loginForm.value.email;
    const password = loginForm.value.password;
    this.store.dispatch(AuthActions.loginStart({ email, password }));
    loginForm.reset();
  }
  onClear(loginForm: NgForm) {
    loginForm.reset();
  }
  private showErrorAlert(error: string) {
    const containerRef = this.placeholder.viewContainerRef;
    containerRef.clear();
    const component = containerRef.createComponent(AlertComponent);
    component.instance.message = error;
    const event = component.instance.closed.subscribe(() => {
      this.store.dispatch(AuthActions.clearError());
      event.unsubscribe();
      containerRef.clear();
    });
  }

  ngOnDestroy(): void {
    this.storeSub.unsubscribe();
  }
}
