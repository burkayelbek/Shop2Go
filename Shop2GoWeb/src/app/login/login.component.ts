import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userLoginForm;
  constructor
  (
    private _formbuilder:FormBuilder,
    private _userService : UserService,
    private _toastrService : ToastrService,
    private _router: Router,
  ) { }

  ngOnInit(): void {
    this.userLoginForm = this._formbuilder.group({
      UserName:['',[
        Validators.required
      ]],
      Password:['',[
        Validators.required,
      ]]
    });
    
    let token = localStorage.getItem('token');
    if( token != null){
      this._router.navigateByUrl('/home');
    }

  }

  get UserName(){
   return this.userLoginForm.get('UserName').value;
  }

  get Password(){
    return this.userLoginForm.get('Password').value;
  }
    
  onSubmit(userLoginForm){
    if(!userLoginForm.valid && this.Password === '' && this.UserName===''){
      this._toastrService.error('Username and password must be filled!','Error');
      return;
    }
    this._userService.LoginUser(userLoginForm.value).subscribe(
    (res:any) =>{
      localStorage.setItem('token',res.token);
      this._router.navigateByUrl('/home');        
    },
    err => {
      this._toastrService.error(err?.error?.message ? err?.error?.message :'User login credentials is not true!','Error');
      
    });
  }

}
