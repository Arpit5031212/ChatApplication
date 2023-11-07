import { formatCurrency } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { Login, Register } from 'src/app/shared/models';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.initForm();
  }

  autoLogin(loginCreds: Login) {
    this.authService.login(loginCreds).subscribe({
      next: (loginResponse: {token: string, id: string}) => {
        alert("login success");
        localStorage.setItem(environment.tokenKey, loginResponse.token);
        localStorage.setItem('userId', loginResponse.id);
      }
      
    })
  }

  onSubmit() {
    console.log(this.registerForm);

    const registerObj: Register = {
      ...this.registerForm.value,
    }

    this.authService.register(registerObj).subscribe({
      next: (res: any) => {
        console.log(res);
        const loginObj = {
          username: registerObj.username,
          email: registerObj.email,
          password: registerObj.password
        }
        this.autoLogin(loginObj);
      },
      error: (error: Error) => {
        console.error(error.message);
      }
    });
  }

  private initForm() {

    let username !: string;
    let firstName !: string;
    let lastName !: string;
    let email !: string;
    let phoneNumber !: string;
    let password !: string;
    let gender: string = "Please Select your gender";
    let dob !: string

    this.registerForm = new FormGroup({
      'username': new FormControl(username, Validators.required),
      'firstName': new FormControl(firstName, Validators.required),
      'lastName': new FormControl(lastName, Validators.required),
      'email': new FormControl(email, [Validators.required, Validators.email]),
      'phoneNumber': new FormControl(phoneNumber, Validators.required),
      'password': new FormControl(password, Validators.required),
      'gender': new FormControl(gender),
      'dob': new FormControl(dob)
    })
  }
}
