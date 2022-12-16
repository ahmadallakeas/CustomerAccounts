import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subscription, map, take } from 'rxjs';
import * as fromApp from '../../store/app.reducer';

@Component({
  selector: 'app-account-start',
  templateUrl: './account-start.component.html',
  styleUrls: ['./account-start.component.css'],
})
export class AccountStartComponent {
  private storeSub: Subscription;
  firstName: string = '';
  name: string = '';
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<fromApp.AppState>
  ) {}

  ngOnInit(): void {
    this.storeSub = this.store
      .select('auth')
      .pipe(
        take(1),
        map((authState) => {
          return authState.customer;
        })
      )
      .subscribe({
        next: (customer) => {
          this.firstName = customer.firstName;
          this.name = customer.firstName + ' ' + customer.lastName;
        },
      });
  }
  onNavigate(dest: string) {
    this.router.navigate([dest], { relativeTo: this.route });
  }

  ngOnDestroy() {}
}
