import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Chats, RecentChats, SendChat } from '../shared/models';
import { SignalrServiceService } from '../shared/SignalrService/signalr-service.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor( private http: HttpClient, private signalrSevice: SignalrServiceService ) { }

  getAllRecentChats(): Observable<RecentChats[]> {
    const userId = localStorage.getItem(environment.userId);
    return this.http.get<RecentChats[]>(`${environment.apiBaseUrl}/Chat/all-recents?id=${userId}`)
  }

  getAllChatsBetweenTwoUsers(user1: number, user2: number): Observable<Chats[]> {
    return this.http.get<Chats[]>(`${environment.apiBaseUrl}/Chat/all?senderId=${user1}&recieverId=${user2}`);
  }

  // sendMessage(messageObj: SendChat): Observable<Chats> {
  //   return this.http.post<Chats>(`${environment.apiBaseUrl}/Chat/send`, messageObj);
  // }

  sendMessage(messageObj: SendChat) {
    this.signalrSevice.hubConnection.invoke<any>("SendChat", messageObj);
  }

  deleteChat(chatId: number) : Observable<any> {
    return this.http.delete<any>(`${environment.apiBaseUrl}/Chat/Delete?chatId=${chatId}&userId=${Number(localStorage.getItem(environment.userId))}`)
  }
}
