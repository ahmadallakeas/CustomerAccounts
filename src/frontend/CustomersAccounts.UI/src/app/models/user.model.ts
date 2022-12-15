import { Transaction } from "./transaction.model";

export interface Userinfo
{
  FirstName:string,
  Surname:string,
  Balance:number,
  Transactions:Transaction[]

}
