import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { TransactionType } from '../models/transaction-type';
import { TransactionDialogComponent } from '../transaction-dialog/transaction-dialog.component';
import { AccountClient } from '../client/account-client';
import { AccountInfoResult } from '../models/account-info-result';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public accountInfo: AccountInfoResult;

  constructor(private router: Router,
    public dialog: MatDialog,
    public accountClient: AccountClient) { }

  ngOnInit(): void {
    this.loadAccountData();
  }

  public openDialog(type: string): void {
    const transactionType = TransactionType[type];
    const dialogRef = this.dialog.open(TransactionDialogComponent, {
      width: '450px',
      data: { type: transactionType }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
    });
  }

  private loadAccountData() {
    this.accountClient.getAccountInfo().subscribe(result => {
      this.accountInfo = result;
    })
  }
  public logout(){
    this.router.navigate(['login']);
    localStorage.removeItem("access_token")
  }
}
