import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import * as fromApp from './store/app.reducer';
import { AuthEffects } from './auth/store/auth.effects';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { HeaderComponent } from './header/header.component';
import { AccountModule } from './account/account.module';
import { AccountEffects } from './account/store/account.effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AuthModule } from './auth/auth.module';

@NgModule({
  declarations: [AppComponent, HeaderComponent],
  imports: [
    BrowserModule,
    RouterModule,
    StoreModule.forRoot(fromApp.appReducer),
    EffectsModule.forRoot([AuthEffects, AccountEffects]),
    AppRoutingModule,
    SharedModule,
    AccountModule,
    AuthModule,
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
