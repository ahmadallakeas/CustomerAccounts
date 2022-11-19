import { Transaction } from "./transaction.model";

export interface Userinfo
{
  firstName:string,
  surname:string,
  balance:number,
  transactions:Transaction[]

}
