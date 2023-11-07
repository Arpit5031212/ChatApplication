import { EventEmitter, Injectable } from "@angular/core";
import { userSearchResult } from "./models";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root"
})

export class DataShareService {
    
    searchedUser = new EventEmitter<userSearchResult>();

    getSearchedUser(): Observable<userSearchResult> {
        return this.searchedUser;
    }

    setSearchedUser(user: userSearchResult) {
        this.searchedUser.emit(user);
    }
}