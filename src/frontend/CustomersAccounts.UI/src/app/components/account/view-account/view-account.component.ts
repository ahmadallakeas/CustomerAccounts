import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-view-account',
  templateUrl: './view-account.component.html',
  styleUrls: ['./view-account.component.css']
})
export class ViewAccountComponent implements OnInit {

  account
  constructor(private router:Router, private activatedRoute:ActivatedRoute) {

  }

  ngOnInit(): void {
    this.account=history.state
    console.log(this.account)
  }

}
