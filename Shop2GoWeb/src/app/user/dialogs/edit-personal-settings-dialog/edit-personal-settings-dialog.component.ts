import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {  MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { PersonalInformationComponent } from '../../personal-information/personal-information.component';

@Component({
  selector: 'app-edit-personal-settings-dialog',
  templateUrl: './edit-personal-settings-dialog.component.html',
  styleUrls: ['./edit-personal-settings-dialog.component.css']
})
export class EditPersonalSettingsDialogComponent implements OnInit {
  userDetailsForm;
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    private _formBuilder:FormBuilder,
    private _userService:UserService,
    private _toastrService:ToastrService,
    public _dialog: MatDialog,
    public dialogRef: MatDialogRef<PersonalInformationComponent>
    
  ) { }

  ngOnInit(): void {
    this.userDetailsForm = this._formBuilder.group({
      UserName:[this.data.UserName,[
      ]],
      FirstName:[this.data.FirstName,[ 
      ]],
      LastName:[this.data.LastName,[
      ]],
      Email:[this.data.Email,[
        Validators.email
      ]],
    });
  }

  UpdateUser(data){
    this._userService.UpdateUser(data).subscribe(
    res =>{
      this._toastrService.success('Profile updated successfully!','Success');
      this.dialogRef.close();
    },
    err=>{
      this._toastrService.error(err.error.message,'Error');
    });
    
  }
  onNoClick(event){
    this.dialogRef.close();
    event.preventDefault();
    return false;
  }

}
