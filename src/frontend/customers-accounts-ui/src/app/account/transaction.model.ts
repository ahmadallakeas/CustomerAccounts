export class Transaction {
  constructor(
    public transactionId: number,
    public accountId: number,
    public transactionName: string,
    public message: string,
    public date: string
  ) {}
}
