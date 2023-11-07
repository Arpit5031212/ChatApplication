import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { environment } from 'src/environments/environment.development';
import { Router } from '@angular/router';
import { SignalrServiceService } from 'src/app/shared/SignalrService/signalr-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  username !: string;
  email !: string;
  password !: string;

  loginForm!: FormGroup;

  constructor( private authService: AuthService, private router: Router, private signalrService: SignalrServiceService ) { }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm() {
    let username!: string;
    let email!: string;
    let password!: string;

    this.loginForm = new FormGroup({
      'username': new FormControl(username, Validators.required),
      'email': new FormControl(email, [Validators.required, Validators.email]),
      'password': new FormControl(password, [Validators.required, Validators.minLength(6), Validators.maxLength(15)])
    })

  }

  onSubmit() {
    if (this.loginForm.valid) {
      const loginObj = this.loginForm.value;
      this.authService.login(loginObj).subscribe({
        next: (res: any) => {
          alert("login success");
          localStorage.setItem(environment.tokenKey, res.token);
          localStorage.setItem(environment.userId, res.id);
          this.signalrService.startConnection();
          this.router.navigate(['/home']);
        },
        error: (error: Error) => {
          console.error(error.message);
        }
      })
    }
  }
}
