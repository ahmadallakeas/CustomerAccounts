import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as AuthActions from '../store/auth.actions';
import * as fromApp from '../../store/app.reducer';
import { Subscription } from 'rxjs';
import { AlertComponent } from 'src/app/shared/alert/alert.component';
import { PlaceholderDirective } from 'src/app/shared/placeholder.directive';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm: FormGroup;
  storeSub: Subscription;
  @ViewChild(PlaceholderDirective, { static: false })
  placeholder: PlaceholderDirective;
  isLoading = false;
  errorMessage: string = null;

  constructor(private store: Store<fromApp.AppState>) {}

  initForm() {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }
  ngOnInit(): void {
    this.initForm();
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
  onSubmit() {
    if (!this.registerForm.valid) {
      return;
    }
    const firstName = this.registerForm.value.firstName;
    const lastName = this.registerForm.value.lastName;
    const email = this.registerForm.value.email;
    const password = this.registerForm.value.password;

    this.store.dispatch(
      AuthActions.signupStart({
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: password,
      })
    );
  }

  onClear() {
    this.registerForm.reset();
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
