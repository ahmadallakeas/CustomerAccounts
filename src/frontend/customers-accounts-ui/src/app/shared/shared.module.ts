import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertComponent } from './alert/alert.component';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { PlaceholderDirective } from './placeholder.directive';
import { ErrorPageComponent } from './error-page/error-page.component';

@NgModule({
  declarations: [
    LoadingSpinnerComponent,
    AlertComponent,
    PlaceholderDirective,
    ErrorPageComponent,
  ],
  imports: [CommonModule],
  exports: [
    LoadingSpinnerComponent,
    AlertComponent,
    PlaceholderDirective,
    CommonModule,
  ],
})
export class SharedModule {}
