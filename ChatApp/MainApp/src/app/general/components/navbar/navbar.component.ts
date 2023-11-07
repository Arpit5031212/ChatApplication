import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';
import { GeneralService } from '../../services/general.service';
import { Chats, userSearchResult } from 'src/app/shared/models';
import { ChatService } from 'src/app/chat/chat.service';
import { DataShareService } from 'src/app/shared/dataShare.service';
import { SignalrServiceService } from 'src/app/shared/SignalrService/signalr-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  searchRes: any[] = [];

  constructor(private router: Router, private generalService: GeneralService, private chatService: ChatService, private dataShareService: DataShareService, private signalrService: SignalrServiceService) { }

  logout() {
    localStorage.removeItem(environment.tokenKey);
    localStorage.removeItem(environment.userId);
    this.signalrService.hubConnection.stop().then(
      () => {
        console.log("server disconnected");
      },
      (err) => {
        console.log(err)
      }
    )
    this.router.navigate(["login"]);
  }

  searchUser(searchStr: string) {
    if (searchStr.length >= 2) {
      this.generalService.SearchUser(searchStr, Number(localStorage.getItem(environment.userId))).subscribe({
        next: (res: any) => {
          this.searchRes = res;
          document.getElementById("dropdown-container")!.style.display = "block";
          console.log(res);
        },
        error: (error: any) => {
          this.searchRes = [];
          console.log(error);
        }
      })
    } else {
      this.searchRes = [];
      document.getElementById("dropdown-container")!.style.display = "none";
    }

  }

  onUserSelect(user: userSearchResult) {
    console.log(user);
    document.getElementById("dropdown-container")!.style.display = "none";
    (document.getElementById("searchBar") as HTMLInputElement).value = "";

    this.dataShareService.setSearchedUser(user);
  }

  hideDropdown() {
    document.getElementById("dropdown-container")!.style.display = "none";
    const searchBar = document.getElementById("searchBar") as HTMLInputElement;
    searchBar.value = "";
  }

}
