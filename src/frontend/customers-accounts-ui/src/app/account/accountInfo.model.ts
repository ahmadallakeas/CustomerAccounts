import { Transaction } from './transaction.model';

export class AccountInfo {
  constructor(
    public firstName: string,
    public surname: string,
    public balance: number,
    public transactions: Transaction[]
  ) {}
}
