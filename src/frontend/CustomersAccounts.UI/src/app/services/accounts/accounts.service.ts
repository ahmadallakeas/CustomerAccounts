import { Injectable } from '@angular/core';
<<<<<<< HEAD
import{HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Userinfo } from 'src/app/models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private http: HttpClient) { }

  ApiUrl:string= environment.ApiUrl;

  getAccountInfo(id: number): Observable<Userinfo>
  {
    return this.http.get<Userinfo>(this.ApiUrl+"/api/account/userinfo/"+id)

  }

=======
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Userinfo } from 'src/app/models/user.model';
import { Observable } from 'rxjs';
import { AddAccount } from 'src/app/models/add-account.model';
import { AccountDto } from 'src/app/models/accountDto.model';

@Injectable({
  providedIn: 'root',
})
export class AccountsService {
  constructor(private http: HttpClient) {}

  private ApiUrl: string = environment.ApiUrl;

  getAccountInfo(id: number):Observable<Userinfo> {
    return this.http.get<Userinfo>(this.ApiUrl + '/api/account/userinfo/' + id);
  }
  AddAccount(AddAccountRequest: AddAccount): Observable<AccountDto> {
    return this.http.post<AccountDto>(
      this.ApiUrl + '/api/account',
      AddAccountRequest
    );
  }
>>>>>>> master
}
