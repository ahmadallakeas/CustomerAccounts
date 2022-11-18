import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/models/customer.model';
import { Userinfo } from 'src/app/models/user.model';
import { AccountsService } from 'src/app/services/accounts/accounts.service';

@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.css'],
})
export class CustomersListComponent implements OnInit {
  constructor(private accountService:AccountsService) {}

  customers: Customer[] = [];
  user: any

  ngOnInit(): void {
    this.accountService.getAccountInfo(7)
    .subscribe(
      {
        next:(info)=>
        {
          console.log(info);
        },
        error:(response)=>
        {
          console.log(response);
        }
      }
    )

  }
}
