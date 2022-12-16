import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountComponent } from './account.component';
import { CreateAccountComponent } from './create-account/create-account.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AccountRoutingModule } from './account-routing.module';
import { AccountStartComponent } from './account-start/account-start.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AccountInterceptorService } from './account-interceptor.service';
import { SharedModule } from '../shared/shared.module';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AccountInfoFormComponent } from './account-info/account-info-form/account-info-form.component';
import { AccountInfoDetailsComponent } from './account-info/account-info-details/account-info-details.component';

@NgModule({
  declarations: [
    AccountComponent,
    CreateAccountComponent,
    AccountInfoComponent,
    AccountStartComponent,
    AccountInfoFormComponent,
    AccountInfoDetailsComponent,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AccountInterceptorService,
      multi: true,
    },
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    FormsModule,
    SharedModule,
    StoreDevtoolsModule.instrument({
      maxAge: 25,
    }),
  ],
})
export class AccountModule {}
