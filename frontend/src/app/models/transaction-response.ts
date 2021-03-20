import { TransactionType } from "./transaction-type"

export class TransactionResponse {
  type: TransactionType;
  amount: number;
  note: string;
  date: Date;

  constructor(init: Partial<TransactionResponse>) {
    Object.assign(this, init);
  }
}