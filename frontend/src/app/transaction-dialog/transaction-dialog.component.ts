import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TransactionRequest } from 'src/app/models/transaction-request-input'
import { OperationClient } from '../client/operation-client';
import { TransactionType } from '../models/transaction-type';

@Component({
  selector: 'app-transaction-dialog',
  templateUrl: './transaction-dialog.component.html',
  styleUrls: ['./transaction-dialog.component.scss']
})

export class TransactionDialogComponent implements OnInit {

  minDate: Date = new Date();
  public transactionForm = new FormGroup({
    operationDate: new FormControl('', [Validators.required]),
    amount: new FormControl('', [Validators.required]),
    note: new FormControl(''),
  });

  constructor(private dateAdapter: DateAdapter<any>,
    public dialogRef: MatDialogRef<TransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TransactionRequest,
    private operationClient: OperationClient,
    private snackBar: MatSnackBar) {
    this.dateAdapter.setLocale('pt-BR');
  }

  public getTransactionTypeDescription() {
    if (!this.data || !this.data.type) {
      return '';
    }
    switch (this.data.type) {
      case TransactionType.Deposit:
        return 'Depósito';
      case TransactionType.Payment:
        return 'Pagamento';
      case TransactionType.Withdrawn:
        return 'Resgate';
      default: return '';
    }
  }

  public onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
  }

  public submit() {
    if (!this.transactionForm.valid) {
      return;
    }

    this.data.amount = this.transactionForm.get('amount').value;
    this.data.note = this.transactionForm.get('note').value;
    this.data.when = this.transactionForm.get('operationDate').value;

    this.operationClient.postOperationRequest(this.data)
      .subscribe(
        result => {
          this.dialogRef.close();
          this.snackBar.open('Solicitação realizada com sucesso. Aguarde o processamento para confirmação.','Fechar');
        },
        error => {
          this.snackBar.open('Não foi possível realizar a solicitação de operação. Tente novamente mais tarde','Fechar');
        }
      )
  }
}
