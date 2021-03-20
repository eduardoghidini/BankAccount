import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AccountInfoResult } from '../models/account-info-result';

@Injectable({
    providedIn: 'root',
})
export class AccountClient {
    
    constructor(private client: HttpClient) { }

    public getAccountInfo(): Observable<AccountInfoResult> {
        var url = `${environment.urlBase}v1/account/me`;
        return this.client.get<AccountInfoResult>(url).pipe(map(result => result));
    }
}