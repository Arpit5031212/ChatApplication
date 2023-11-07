import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {

  constructor( private http: HttpClient ) { }

  SearchUser(searchStr: string, currentUserId: number): Observable<any> {
    return this.http.get<any>(`${environment.apiBaseUrl}/General/search?searchStr=${searchStr}&currentUserId=${currentUserId}`);
  }

  getUserFromSearch(id: number): Observable<any> {
    return this.http.get<any>(`${environment.apiBaseUrl}`);
  }
}
