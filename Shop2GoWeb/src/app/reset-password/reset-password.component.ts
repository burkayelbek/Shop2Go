import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IResetPasswordModel } from '../shared/interfaces';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm;
  UserID;
  Email;
  
  constructor
  (
    private _formbuilder:FormBuilder,
    private _route:ActivatedRoute,
    private _userService:UserService,
    private _toastrService:ToastrService

  ) { }

  ngOnInit(): void {
    this.resetPasswordForm = this._formbuilder.group({
      Password:['',[
        Validators.required,
      ]]
    });

    this._route.queryParamMap.subscribe((res:any) => {
      this.UserID = res.params.UserID;
      this.Email = res.params.Email;
    });

  }

  ResetPassword(){
    if(this.UserID && this.UserID === '' && this.Email && this.Email === '' && !this.resetPasswordForm.valid){
      this._toastrService.error('Error!','Error');
      return;
    }
    var data:IResetPasswordModel = {
        UserID: this.UserID,
        Email:this.Email,
        Password:this.resetPasswordForm.get('Password').value
    }
    this._userService.ResetPassword(data).subscribe(
      (res:any) =>{
        this._toastrService.success(res.message,'Success');
    },
    err=>{
      console.log(err);
      this._toastrService.error('Password could not changed!','Error');
    });
  }



}
