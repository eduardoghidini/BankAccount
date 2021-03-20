import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, } from '@angular/forms';
import { AuthClient } from 'src/app/client/auth-client';
import { OperationClient } from 'src/app/client/operation-client';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm = new FormGroup({
    user: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(
    private router: Router,
    private authClient: AuthClient,
    private operationClient: OperationClient,
    private snackBar: MatSnackBar) {
  }

  public ngOnInit(): void {
  }

  public onSubmit() {
    if (this.loginForm.valid) {
      const userName = this.loginForm.get('user').value;
      const password = this.loginForm.get('password').value;
      this.authClient.login(userName, password)
        .subscribe(
          result => {
            localStorage.setItem('access_token', result.token);
            this.router.navigate(['home']);
            
          },
          error => {
            this.snackBar.open('Usuário não encontrado.','Fechar');
          }
        );
    }
  }

  public navigateToHome() {
    this.router.navigate(['home']);
  }

}
