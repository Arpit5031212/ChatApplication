import { Component, EventEmitter, Output } from '@angular/core';
import { FileUploadService } from './file-upload.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent {

  fileName !: string;
  imageData !: FormData;

  @Output() imgPreviewSrc = new EventEmitter<string>();
  
  constructor( private fileUploadService: FileUploadService ) { }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if(file) {
      this.fileName = file.name;
      const formData = new FormData();
      formData.append("imageFile", file);
      this.imageData = formData;

      this.imgPreviewSrc.emit(URL.createObjectURL(event.target.files[0]));
    }
  }

  onConfirmUpload() {

    this.fileUploadService.uploadImage(this.imageData).subscribe({
      next: (res: FormData) => {
        console.log(res);
      }, 
      error: (error: Error) => {
        console.log(error.message);
      }
    })
    
  }
}
