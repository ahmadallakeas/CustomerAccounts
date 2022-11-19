import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddAccountComponent } from './components/account/add-account/add-account.component';
import { UserInfoComponent } from './components/account/user-info/user-info.component';
import { ViewAccountComponent } from './components/account/view-account/view-account.component';

const routes: Routes = [
  {
    path:'',
    component: AddAccountComponent
  },
  {
    path:'viewAccount',
    component: ViewAccountComponent
  },
  {
    path:'user-info',
    component:UserInfoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
