import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountsService } from 'src/app/services/accounts/accounts.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css'],
})
export class UserInfoComponent implements OnInit {
  constructor(private accountService: AccountsService) {}
  accountId;
  errorMessage;
  userInfo;
  ngOnInit(): void {}
  getUserInfo(form: NgForm) {
    this.accountService.getAccountInfo(this.accountId).subscribe({
      next: (user) => {
        console.log(user);
        this.userInfo=user;
        form.reset();
      },
      error: (respone) => {
        console.log(respone);
        this.errorMessage = respone.error;
      },
    });
  }
}
