import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddAccountComponent } from './components/account/add-account/add-account.component';
import { ViewAccountComponent } from './components/account/view-account/view-account.component';
import { UserInfoComponent } from './components/account/user-info/user-info.component';

@NgModule({
  declarations: [
    AppComponent,

    AddAccountComponent,
      ViewAccountComponent,
      UserInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
