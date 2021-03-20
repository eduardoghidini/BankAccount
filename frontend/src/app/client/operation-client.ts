import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { TransactionRequest } from 'src/app/models/transaction-request-input';
import { TransactionResponse } from 'src/app/models/transaction-response';
import { PagedResult } from 'src/app/models/paged-result';
import { environment } from 'src/environments/environment';
import { OperationRequestResult } from 'src/app/models/operation-request-result';

@Injectable({
  providedIn: 'root',
})
export class OperationClient {

  constructor(private client: HttpClient) { }

  public getOperations(pageSize: number, pageNumber: number): Observable<PagedResult<TransactionResponse>> {
    var url = `${environment.urlBase}v1/operation/list?pageSize=${pageSize}&page=${pageNumber}`;
    return this.client.get(url).pipe(
      map(response => {
        const transactions = response['data'].map((item: any) => {
          return new TransactionResponse({
            amount: item['amount'],
            note: item['note'],
            type: item['type'],
            date: item['date']
          })
        })
        return new PagedResult<TransactionResponse>({
          items: transactions,
          pageNumber: response['pageNumber'],
          pageSize: response['pageSize'],
          totalRecordNumber: response['totalRecords']
        });
      }));
  }

  public getOperationRequest(pageSize: number, pageNumber: number): Observable<PagedResult<OperationRequestResult>> {
    var url = `${environment.urlBase}v1/operation-request/list?pageSize=${pageSize}&page=${pageNumber}`;
    return this.client.get(url).pipe(
      map(response => {
        const transactions = response['data'].map((item: any) => {

          return new OperationRequestResult({
            id: item['id'],
            requestedDate: item['requestedDate'],
            processedDate: item['processedDate'],
            type: item['type'],
            operationResponseMessage: item['operationResponseMessage'],
            status: item['status'],
            amount: item['amount'],
          })
        })
        return new PagedResult<OperationRequestResult>({
          items: transactions,
          pageNumber: response['pageNumber'],
          pageSize: response['pageSize'],
          totalRecordNumber: response['totalRecords']
        });
      }));
  }

  public postOperationRequest(transaction: TransactionRequest): Observable<any> {
    var request = {
      amount: transaction.amount,
      type: transaction.type,
      note: transaction.note,
      operationDate: transaction.when
    };

    var url = `${environment.urlBase}v1/operation-request`;
    return this.client.post(url, request).pipe();
  }
}