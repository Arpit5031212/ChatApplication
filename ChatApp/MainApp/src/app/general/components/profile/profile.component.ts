import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProfileService } from '../../services/profile.service';
import { Register, UserProfile } from 'src/app/shared/models';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  profileImage!: string;
  registerForm!: FormGroup;
  userProfile!: Register;

  constructor( private profileService: ProfileService ) {   }

  ngOnInit() {
    this.initForm();
    this.getProfile();
  }

  setPreviewSrc(event: string) {
    this.profileImage = event;
  }

  onSubmit() {
    this.profileService.updateProfile(this.registerForm.value).subscribe({
      next: (res: Register) => {
        this.registerForm.patchValue(res);
        console.log(res);
      },
      error: (error: Error) => {
        this.registerForm.reset();
        console.log(error);
      }
    });
  }

  onCancel() {
    this.getProfile();
  }

  

  getProfile() {
    this.profileService.getUser().subscribe({
      next: (res: Register) => {

        const formattedDate = this.getDateInCorrectFormat(res.dateOfBirth);

        this.userProfile = res;
        console.log(formattedDate)
        this.userProfile.dateOfBirth = formattedDate;

        console.log(this.userProfile.dateOfBirth)
        
        this.registerForm.patchValue(this.userProfile);

        if(res.imageName != null) {
          this.profileImage = `${environment.apiBaseUrl}/Profile/${res.imageName}`;
        }
      },
      error: (error: Error) => {
        console.log(error);
      }
    })
  }

  getDateInCorrectFormat(date: string): string {
    const dob = new Date(date); 

    const day = dob.getDate();
    const month = dob.getMonth() + 1;
    const year = dob.getFullYear();

    let newdate = "";
    if(month < 10) {
      newdate = year + "-0" + month + "-" + day;
    }

    if(day < 10) {
      newdate = year + "-" + month + "-0" + day;
    }

    if(month < 10 && day < 10) {
      newdate = year + "-0" + month + "-0" + day;
    }

    return newdate;
  }

  private initForm() {

    this.registerForm = new FormGroup({
      'username': new FormControl('', Validators.required),
      'firstName': new FormControl('', Validators.required),
      'lastName': new FormControl('', Validators.required),
      'email': new FormControl('', [Validators.required, Validators.email]),
      'phoneNumber': new FormControl('', Validators.required),
      'password': new FormControl('', Validators.required),
      'gender': new FormControl('', Validators.required),
      'dateOfBirth': new FormControl('', Validators.required)
    })
  }

}
