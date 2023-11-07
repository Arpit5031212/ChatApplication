import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ChatService } from 'src/app/chat/chat.service';
import { DataShareService } from 'src/app/shared/dataShare.service';
import { RecentChats, userSearchResult } from 'src/app/shared/models';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent implements OnInit {

  recentChats: RecentChats[] = [];
  selected: number = 0;

  mostRecentContactedUserId: number = 0;

  @Output() UserSelected = new EventEmitter<{ userId1: number, userId2: number }>();
  mostRecentUser: number = 0;

  @Input() public set getMostRecentlyContactedUser(MostRecentlyContactedUserId: number) {
    if (this.mostRecentUser != MostRecentlyContactedUserId) {
      this.getRecentChats();
    }
  }
  

  constructor(private chatService: ChatService, private dataShareService: DataShareService) { }

  ngOnInit() {
    this.getRecentChats();
    this.dataShareService.getSearchedUser().subscribe({
      next: (res: userSearchResult) => {
        if (this.checkSearchedUserInRecentChats(res)) {
          console.log("if block")
          this.setIds(res.id, Number(localStorage.getItem(environment.userId)));
        } else {
          console.log("else block")

          const tempRecentChatObj: RecentChats = {
            firstName: res.firstName,
            lastName: res.lastName,
            username: res.username,
            chatContent: "",
            senderId: Number(localStorage.getItem(environment.userId)),
            recieverId: res.id,
            createdAt: new Date(),
          }
          this.setIds(res.id, Number(localStorage.getItem(environment.userId)));
          this.recentChats.unshift(tempRecentChatObj);
          this.isSelected(tempRecentChatObj);
        }
      },
      error: (err: Error) => {
        console.log(err);
      }
    })
  }

  checkSearchedUserInRecentChats(user: userSearchResult): boolean {
    const userExists = this.recentChats.find(u => u.senderId == user.id || u.recieverId == user.id) !== undefined;
    console.log(`user exists in recents = ${userExists}`);
    return userExists;
  }

  setIds(id1: number, id2: number) {
    this.UserSelected.emit({ userId1: id1, userId2: id2 })
    const loggedUser = Number(localStorage.getItem(environment.userId));
    if (id1 == loggedUser) {
      this.selected = id2;
    } else {
      this.selected = id1;
    }
  }

  isSelected(chat: any) {
    if (chat.recieverId == this.selected || chat.senderId == this.selected) {
      return true;
    }
    return false;
  }

  getRecentChats() {
    this.chatService.getAllRecentChats().subscribe({
      next: (res: RecentChats[]) => {
        console.log(res);
        this.recentChats = res;
        this.mostRecentContactedUserId = this.recentChats[0].senderId !== Number(localStorage.getItem(environment.userId)) ? this.recentChats[0].senderId : this.recentChats[0].recieverId;
      },
      error: (err: Error) => {
        console.log(err);
      }
    })
  }

}
