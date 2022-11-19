import { Transaction } from "./transaction.model";

export interface AccountDto
{
  accountId:number,
  customerId:number,
  balance:number,
  transactions: Transaction[]
}
