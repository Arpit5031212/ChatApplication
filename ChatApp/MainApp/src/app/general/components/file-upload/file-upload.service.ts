import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment.development";


@Injectable({
    providedIn: 'root'
})

export class FileUploadService {

    constructor( private http: HttpClient ) { }

    uploadImage(imageData: FormData): Observable<FormData> {
        const userId: number = Number(localStorage.getItem(environment.userId));
        return this.http.put<FormData>(`${environment.apiBaseUrl}/Profile/update-image?id=${userId}`, imageData);
    }
}