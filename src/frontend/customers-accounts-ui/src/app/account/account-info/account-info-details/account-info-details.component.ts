import { Component, Input } from '@angular/core';
import { AccountInfo } from '../../accountInfo.model';

@Component({
  selector: 'app-account-info-details',
  templateUrl: './account-info-details.component.html',
  styleUrls: ['./account-info-details.component.css'],
})
export class AccountInfoDetailsComponent {
  @Input() accountInfo: AccountInfo;
}
