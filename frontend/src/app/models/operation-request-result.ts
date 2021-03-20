import { OperationRequestStatus } from './operation-request-status';
import { TransactionType } from './transaction-type';

export class OperationRequestResult {
  id: number;
  requestedDate: Date;
  processedDate: Date;
  type: TransactionType;
  operationResponseMessage: string;
  status: OperationRequestStatus;
  amount: number;

  constructor(init: Partial<OperationRequestResult>) {
    Object.assign(this, init);
  }
}