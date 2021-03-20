import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginResult } from '../models/login-result';

@Injectable({
  providedIn: 'root',
})
export class AuthClient {
  constructor(private client: HttpClient) { }

  public login(user: string, pass: string): Observable<LoginResult> {

    var url = `${environment.urlBase}v1/auth/login`;

    const request = {
      userName: user,
      password: pass,
    };

    return this.client.post<LoginResult>(url, request).pipe(map(result => result));
  }
}