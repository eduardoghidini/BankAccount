import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { OperationClient } from '../client/operation-client';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})

export class TransactionListComponent implements AfterViewInit {

  displayedColumns: string[] = ['date','type', 'amount', 'note'];
  public pageIndex: number = 0;
  public pageSize: number = 20;
  public pageSizeOptions: number[] = [1, 5, 10];
  public totalRecordNumber: number;

  public dataSource;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private operationClient: OperationClient) { }

  ngAfterViewInit() {
    this.loadData();
  }

  public onPaginateChange(event) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  public loadData() {
    this.operationClient.getOperations(this.pageSize, this.pageIndex)
      .subscribe(result => {
        console.log(result);
        this.dataSource = result.items;
        this.totalRecordNumber = result.totalRecordNumber;
      })
  }
}


