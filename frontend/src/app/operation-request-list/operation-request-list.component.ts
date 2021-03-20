import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { OperationClient } from '../client/operation-client';

@Component({
  selector: 'app-operation-request-list',
  templateUrl: './operation-request-list.component.html',
  styleUrls: ['./operation-request-list.component.scss']
})
export class OperationRequestListComponent implements AfterViewInit {

  displayedColumns: string[] = ['requestedDate','type', 'amount', 'status', 'processedDate'];
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

  public onPageChange(pageEvent: PageEvent) {
    console.log(pageEvent);
  }

  public onPaginateChange(event) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  public loadData() {
    this.operationClient.getOperationRequest(this.pageSize, this.pageIndex)
      .subscribe(result => {
        console.log(result);
        this.dataSource = result.items;
        this.totalRecordNumber = result.totalRecordNumber;
      })
  }
}


