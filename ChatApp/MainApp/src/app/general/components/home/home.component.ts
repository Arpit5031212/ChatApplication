import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Route, Router } from '@angular/router';
import { ChatService } from 'src/app/chat/chat.service';
import { GeneralService } from '../../services/general.service';
import { environment } from 'src/environments/environment.development';
import { Chats, SendChat } from 'src/app/shared/models';
import { SignalrServiceService } from 'src/app/shared/SignalrService/signalr-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  chatsBetweenUsers: Chats[] = [];

  show: boolean = false;
  showChatOptions: boolean = false;
  chatSelected: number = 0;
  loggedInUser: number = Number(localStorage.getItem(environment.userId));
  theOtherUser: number = 0;

  repliedTo: number = 0;
  messageBeingReplied: string = "";

  MostRecentlyContactedUserId!: number;

  users = {
    userId1: 0,
    userId2: 0
  }

  constructor(private router: Router, private chatService: ChatService, private generalService: GeneralService, private signalrService: SignalrServiceService) {

  }

  toggleChatOptions(chatId: number) {
    this.showChatOptions = !this.showChatOptions;
    this.chatSelected = chatId;
  }

  showOptions(chatId: number) {
    return chatId == this.chatSelected && this.showChatOptions;
  }

  ngOnInit() {
    const chatBox = document.getElementById("chat-box");
    const maxHeight = 100;
    if (chatBox) {
      chatBox.oninput = function () {
        chatBox.style.height = "";

        chatBox.style.height = Math.min(chatBox.scrollHeight, maxHeight) + "px";
        if (Number(chatBox.style.height.substring(0, chatBox.style.height.length - 2)) >= maxHeight) {
          chatBox.style.overflow = "visible";
        } else {
          chatBox.style.overflow = "hidden";
        }
      }
    }
    //this.signalrService.startConnection();

    this.signalrService.hubConnection.on("RecieveMessage", (chat: any) => {
      this.chatsBetweenUsers.push(chat);
      console.log("message sent");
    })
  }

  getAllChatsBetweenTwoUsers(user1: number, user2: number) {
    this.chatService.getAllChatsBetweenTwoUsers(user1, user2).subscribe({
      next: (res: Chats[]) => {
        console.log(res);
        this.chatsBetweenUsers = res;
      },
      error: (error: Error) => {
        console.log(error);
      }
    })
  }

  getClass(chat: any) {
    if (chat.senderId == localStorage.getItem(environment.userId)) {
      return "sent-chats"
    }
    return "recieved-chats";
  }



  sendMessage(message: HTMLTextAreaElement) {

    if (message.value != "") {

      const messageObj = {
        senderId: this.loggedInUser,
        recieverId: this.theOtherUser,
        chatType: 0,
        chatContent: message.value,
        repliedTo: 0
      }

      if(this.repliedTo !== 0) {
        messageObj.repliedTo = this.repliedTo;
      }

      this.chatService.sendMessage(messageObj);
      
      const chatBeingReplied = document.getElementById("chat-being-replied");
      if (chatBeingReplied != null) {
        chatBeingReplied.innerText = "";
      }
      message.value = "";
      this.repliedTo = 0;


      // this.chatService.sendMessage(messageObj).subscribe({
      //   next: (res: Chats) => {
      //     console.log(res);
      //     this.chatsBetweenUsers.push(res);
      //     this.MostRecentlyContactedUserId = this.theOtherUser;
      //   },
      //   error: (error: Error) => {
      //     console.log(error);
      //   }
      // })

      
    }
  }

  editChat(chat: Chats) {

  }

  deleteChat(chatId: number) {
    this.chatService.deleteChat(chatId).subscribe({
      next: (res: any) => {
        if (res) {
          const remainingChats = this.chatsBetweenUsers.filter(c => c.id != chatId);
          this.chatsBetweenUsers = remainingChats;
        }
      }
    })
  }

  replyToChat(chat: Chats) {
    const chatBox = document.getElementById("chat-box");
    const chatBeingReplied = document.getElementById("chat-being-replied");
    if (chatBeingReplied != null) {
      this.repliedTo = chat.repliedTo;
      chatBeingReplied.innerText = chat.chatContent;
      this.messageBeingReplied = chat.chatContent;
    }
    chatBox?.focus();
  }

  onUserSelect(event: { userId1: number; userId2: number; }) {
    this.users.userId1 = event.userId1;
    this.users.userId2 = event.userId2;
    this.show = true;

    if (this.users.userId1 == this.loggedInUser) {
      this.theOtherUser = this.users.userId2;
    } else {
      this.theOtherUser = this.users.userId1;
    }

    this.getAllChatsBetweenTwoUsers(this.users.userId1, this.users.userId2);
  }
}
