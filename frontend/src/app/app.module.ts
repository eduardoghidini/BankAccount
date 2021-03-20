import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HomeComponent } from './home/home.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from "@auth0/angular-jwt";
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { TransactionDialogComponent } from './transaction-dialog/transaction-dialog.component';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { CurrencyMaskInputMode, NgxCurrencyModule } from "ngx-currency";
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatChipsModule } from '@angular/material/chips';
import { LOCALE_ID } from '@angular/core';
import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { OperationRequestListComponent } from './operation-request-list/operation-request-list.component';

export const customCurrencyMaskConfig = {
  align: "left",
  allowNegative: true,
  allowZero: true,
  decimal: ",",
  precision: 2,
  prefix: "R$ ",
  suffix: "",
  thousands: ".",
  nullable: true,
  min: null,
  max: null,
  inputMode: CurrencyMaskInputMode.FINANCIAL
};

export function tokenGetter() {
  return localStorage.getItem("access_token");
}

registerLocaleData(localePt, 'pt');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    TransactionListComponent,
    TransactionDialogComponent,
    OperationRequestListComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSliderModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatDividerModule,
    MatIconModule,
    MatDialogModule,
    MatChipsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule,
    MatTabsModule,
    NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
    JwtModule.forRoot({
      config: {
        headerName: 'Authorization',
        authScheme: 'Bearer ',

        tokenGetter: () => {
          return localStorage.getItem("access_token");
        },
        allowedDomains: ["localhost:44379"],
        disallowedRoutes: ["localhost:44379/bank-account/v1/auth/login"],
      },
    }),
  ],
  providers: [MatDatepickerModule,
    { provide: LOCALE_ID, useValue: 'pt-BR' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
