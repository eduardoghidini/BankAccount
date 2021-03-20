import { TransactionType } from "./transaction-type";

export interface TransactionRequest {
    when: Date;
    amount: number;
    type: TransactionType;
    note: string;
}