import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { PersonalInformationComponent } from '../../personal-information/personal-information.component';

import { faKey } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.css']
})
export class ChangePasswordDialogComponent implements OnInit {
  changePasswordForm;
  passwordIcon = faKey;
  constructor
  (
    private _userService:UserService,
    private _formBuilder:FormBuilder,
    private _toastrService:ToastrService,
    public dialogRef: MatDialogRef<PersonalInformationComponent>
  ) { }

  ngOnInit(): void {
    this.changePasswordForm = this._formBuilder.group({
      CurrentPassword:['',[
        Validators.required
      ]],
      Password:['',[
        Validators.minLength(6),
        Validators.required
      ]],
      ConfirmPassword:['',[
        Validators.minLength(6),
        Validators.required
      ]],
    },{validator:this.checkPassword});
  }

  get Password(){
    return this.changePasswordForm.get('Password');
  }

  get ConfirmPassword(){
    return this.changePasswordForm.get('ConfirmPassword');
  }

  ChangePassword(data){
    this._userService.ChangePassword(data).subscribe(
      res => {
        this._toastrService.success('Password changed successfully!','Success');
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

  private checkPassword(passwordControl: FormGroup) {
    const passwordInput = passwordControl.controls['Password'];
    const passwdCtrlInput = passwordControl.controls['ConfirmPassword'];
    if (passwordInput.value !== passwdCtrlInput.value) {
      return passwdCtrlInput.setErrors({passwordMismatch: true});
    }
    else
      return passwdCtrlInput.setErrors(null);
}

}
