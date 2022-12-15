import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountDto } from 'src/app/models/accountDto.model';
import { AddAccount } from 'src/app/models/add-account.model';
import { AccountsService } from 'src/app/services/accounts/accounts.service';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css'],
})
export class AddAccountComponent implements OnInit {
  constructor(
    private accountService: AccountsService,
    private router: Router
  ) {}

  addAccount: AddAccount = {
    customerId: '',
    initialCredit: '',
  };
  errorMessage;
  successMessage$;
  account

  ngOnInit(): void {}
  addEmployee(form: NgForm) {
    this.accountService.AddAccount(this.addAccount).subscribe({
      next: (account) => {
        this.successMessage$ =
          'Account for customer with id ' +
          account.customerId +
          ' has been successfully created and accountId = ' +
          account.accountId;

        form.reset();
        this.account=account
        //console.log(this.account);
      },
      error: (respone) => {

        this.errorMessage = respone.error;
      },
    });
  }
  navigateToViewAccount() {
    this.router.navigateByUrl('/viewAccount', { state: this.account });
  }
}
