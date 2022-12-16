import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountComponent } from './account.component';
import { AuthGuard } from '../auth/auth.guard';
import { Routes, RouterModule } from '@angular/router';
import { CreateAccountComponent } from './create-account/create-account.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AccountStartComponent } from './account-start/account-start.component';

export const routes: Routes = [
  {
    path: '',
    component: AccountComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: AccountStartComponent },
      {
        path: 'create',
        component: CreateAccountComponent,
      },
      {
        path: 'info',
        component: AccountInfoComponent,
      },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountRoutingModule {}
