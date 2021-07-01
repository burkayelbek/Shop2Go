import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordField;
  constructor
  (
    private _formbuilder:FormBuilder,
    private _userService:UserService,
    private _toastrService:ToastrService

  ) { }

  ngOnInit(): void {
    this.forgotPasswordField = this._formbuilder.group({
      Email:['',[
        Validators.required,
        Validators.email
      ]]
    });
    
  }

  ForgotPassword(Email){
    this._userService.ForgotPassword(Email).subscribe(
      (res:any)=>{
          this._toastrService.success(res.message,'Success');
      },
      err => {
          this._toastrService.error(err.error.message,'Error');
      }
    );
  }

}
