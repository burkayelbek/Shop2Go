import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormControl,FormGroup,Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { faUserTag } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';
import { NavbarComponent } from 'src/app/navbar/navbar.component';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  userSignUpForm;

  userNameIcon = faUserTag;
  hide=true;

  constructor
  (
    private _formBuilder : FormBuilder,
    private _toastrService:ToastrService,
    private _userService: UserService,
    public dialogRef: MatDialogRef<NavbarComponent>
  ) { }

  ngOnInit(): void {
    this.userSignUpForm = this._formBuilder.group({
      UserName:['',[
        Validators.required
      ]],
      FirstName:['',[ 
        Validators.required
      ]],
      LastName:['',[
        Validators.required
      ]],
      Email:['',[
        Validators.required,
        Validators.email
      ]],
      Password:['',[
        Validators.required,
        Validators.minLength(6)
      ]],
      ConfirmPassword:['',[
        Validators.required,
        Validators.minLength(6),
      ]]
    },{validator: this.checkPassword});
  }

  get Password(){
    return this.userSignUpForm.get('Password');
  }

  get ConfirmPassword(){
    return this.userSignUpForm.get('ConfirmPassword');
  }

  get FirstName(){
    return this.userSignUpForm.get('FirstName');
  }

  get LastName(){
    return this.userSignUpForm.get('LastName');
  }

  get UserName(){
    return this.userSignUpForm.get('UserName');
  }

  get Email(){
    return this.userSignUpForm.get('Email');
  }

  PostUser(model){
    this._userService.PostUser(model).subscribe(
    res =>{
      if(this.userSignUpForm.valid){
        this._toastrService.success('Register performed successfully!','Success');
        this.userSignUpForm.reset();
        this.dialogRef.close();
      }},  
      err =>{
        this._toastrService.error(err.error.message,'Registration Failed');
      }
    )
  
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