import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
<<<<<<< HEAD
=======
import { FormsModule } from '@angular/forms';
>>>>>>> master
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
<<<<<<< HEAD
import { CustomersListComponent } from './components/customers-list/customers-list/customers-list.component';
=======
import { AddAccountComponent } from './components/account/add-account/add-account.component';
import { ViewAccountComponent } from './components/account/view-account/view-account.component';
import { UserInfoComponent } from './components/account/user-info/user-info.component';
>>>>>>> master

@NgModule({
  declarations: [
    AppComponent,
<<<<<<< HEAD
    CustomersListComponent
=======

    AddAccountComponent,
      ViewAccountComponent,
      UserInfoComponent
>>>>>>> master
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
<<<<<<< HEAD
    HttpClientModule
=======
    HttpClientModule,
    FormsModule
>>>>>>> master
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
