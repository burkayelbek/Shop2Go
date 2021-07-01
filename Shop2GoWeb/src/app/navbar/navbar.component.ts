import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { faUserCog } from '@fortawesome/free-solid-svg-icons';
import { faShoppingBasket } from '@fortawesome/free-solid-svg-icons';
import { faComment } from '@fortawesome/free-solid-svg-icons';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RegistrationComponent } from '../user/registration/registration.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  adminIcon = faUserCog;
  productIcon = faShoppingBasket;
  messageIcon = faComment;
  userId;

  constructor
  (
    private _router : Router,
    private _userService:UserService,
    public _dialog : MatDialog
    
  ) { }

  ngOnInit(): void {
  }

  onLogout(){
    localStorage.removeItem('token');
    this._router.navigate(['/Login']);
  }

  loginCheck(){
    return this._userService.loginCheck();
  }

  roleMatchAdmin(){
    return this._userService.roleMatch(['Admin']);
  }

  redirectToProfile(){
    var token = localStorage.getItem('token') as string | undefined;
    const helper = new JwtHelperService();
    this.userId = helper.decodeToken(token).UserID;
    this._router.navigate(['/User-Profile/',this.userId]);
  }

  openSignUpDialog(){
    let dialogRef = this._dialog.open(RegistrationComponent,{
      disableClose:false,
      autoFocus:false
    });
    dialogRef.afterClosed().subscribe(
    res => {
     
    });
  }
}