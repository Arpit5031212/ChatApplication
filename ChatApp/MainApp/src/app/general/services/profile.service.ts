import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Register, UserProfile } from 'src/app/shared/models';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor( private http: HttpClient ) { }

  getUser(): Observable<Register> {
    const userId: number = Number(localStorage.getItem(environment.userId));
    return this.http.get<Register>(`${environment.apiBaseUrl}/Profile/user?id=${userId}`);
  }

  updateProfile(updateProfileObj: Register): Observable<Register> {
    const userId: number = Number(localStorage.getItem(environment.userId));
    return this.http.put<Register>(`${environment.apiBaseUrl}/Profile/update-profile?id=${userId}`, updateProfileObj);
  }

  // getProfileImageUrl(imageName: string): string {
  //   return `${environment.apiBaseUrl}/Profile/${imageName}`;
  // }
}
