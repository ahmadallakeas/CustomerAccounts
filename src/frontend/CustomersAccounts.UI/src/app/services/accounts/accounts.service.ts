import { Injectable } from '@angular/core';
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

}
