import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { IGetUser } from 'src/app/shared/interfaces';
import { UserService } from 'src/app/shared/user.service';
import { ChangePasswordDialogComponent } from '../dialogs/change-password-dialog/change-password-dialog.component';
import { EditPersonalSettingsDialogComponent } from '../dialogs/edit-personal-settings-dialog/edit-personal-settings-dialog.component';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrls: ['./personal-information.component.css']
})
export class PersonalInformationComponent implements OnInit {
  @ViewChild('fileInput')
  fileInput: ElementRef;

  userDetails:any;
  userInformationForm;
  srcResult;
  base64Data:string;
  ProfilePicture:any;
  userEditIcon = faUserEdit;
  constructor
    (
      private _userService: UserService,
      public _dialog: MatDialog,
      private _formBuilder:FormBuilder,
      private _toastrService:ToastrService
    ) { }

  ngOnInit(): void {
    this.GetUser();
    this.GetProfileImage();
  }

  createForm(){
    this.userInformationForm  = this._formBuilder.group({
      UserName:[{value:this.userDetails.UserName,disabled:true},[]],
      FirstName:[{value:this.userDetails.FirstName,disabled:true},[]],
      LastName:[{value:this.userDetails.LastName,disabled:true},[]],
      Email:[{value:this.userDetails.Email,disabled:true},[]],
    });
 }

  GetUser(){
    this._userService.GetUser().subscribe(
      res => {
          this.userDetails = res as IGetUser[];
          this.createForm();
      },
      err => {
          console.log(err);
      }
    )
  }

  openEditPersonalDialog(){
    let dialogRef = this._dialog.open(EditPersonalSettingsDialogComponent,{
      data:{
        UserName:this.userDetails.UserName,
        FirstName:this.userDetails.FirstName,
        LastName:this.userDetails.LastName,
        Email:this.userDetails.Email,
      }
    });
    dialogRef.afterClosed().subscribe(
      res => {
        this.GetUser();
      });
  }

  openChangePasswordDialog(){
    let dialogRef = this._dialog.open(ChangePasswordDialogComponent,{
    });
    dialogRef.afterClosed().subscribe(res => {
    });
  }

  AddProfilePicture(){
    const model = {ProfilePicture:this.base64Data}
    this._userService.AddProfilePicture(model).subscribe(
    res => {
      this.fileInput.nativeElement.value="";
      this.GetProfileImage();
    },
    err=>{
      console.log(err);
    });
  }

  GetProfileImage(){
    this._userService.GetProfileImage().subscribe(
    (res:any) => {
      this.ProfilePicture = res.ProfilePicture;
    },
    err=>{
      console.log(err);
    });
  }

  handleFile(fileEvent: any, fileInput: any) {
  const inputNode: any = document.querySelector('#file');
  if (typeof (FileReader) !== 'undefined') {
    const reader = new FileReader();
    reader.readAsArrayBuffer(inputNode.files[0]);
    reader.onload = (e: any) => {
      this.srcResult = e.target.result;
      this.ConvertBufferToBase64(this.srcResult);
      this.AddProfilePicture();
      }
    }
  }
  ConvertBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer); 
    for (var i = 0; i < bytes.byteLength; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    this.base64Data = window.btoa(binary);
  }
  

}
