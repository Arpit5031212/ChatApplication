import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection } from '@microsoft/signalr';
import { environment } from 'src/environments/environment.development';
import { Chats } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SignalrServiceService {

  constructor() { }
  
  hubConnection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7248/api/chatHub",{
    skipNegotiation:true,
    transport : signalR.HttpTransportType.WebSockets
  })
  .withAutomaticReconnect()
  .build();

  startConnection = () => {
    console.log("connection started1");
    this.hubConnection.start().then(
      () => {
        console.log("connected successfully");
        this.registerUser();
      },
      (err) => {
        console.log(err.message);
      }
    );

  }

  registerUser() {
    this.hubConnection.invoke("AddUser", Number(localStorage.getItem(environment.userId)));
  }

  sendChat(chat: Chats) {
    this.hubConnection.invoke("SendChat", chat);
  }
  
}

