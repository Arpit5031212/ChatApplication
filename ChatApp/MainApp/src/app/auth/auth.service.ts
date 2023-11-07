import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Login, Register } from '../shared/models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor( private http: HttpClient ) { }

  login(loginObj: Login): Observable<any> {
    return this.http.post(`${environment.apiBaseUrl}/Account/Login`, loginObj);
  }

  register(registerObj: Register): Observable<any> {
    return this.http.post(`${environment.apiBaseUrl}/Account/Register`, registerObj);
  }

  logout() {
    
  }
}
