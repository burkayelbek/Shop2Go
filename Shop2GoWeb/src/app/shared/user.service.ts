import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { IDeleteTicketModel, IProductId } from './interfaces';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly API_link = 'http://localhost:62214/api'; //Allow communicating route for backend api

  constructor(
    private http:HttpClient,
    private _toastrService : ToastrService,
    private _router : Router
    ) { }

  loginCheck(){
    const token = localStorage.getItem('token');
    const jwtService = new JwtHelperService();
 
    if(token != null){
      const tokenControl = jwtService.isTokenExpired(token);
 
      if(tokenControl){
        this._toastrService.error('Something is wrong!','Error!');
        localStorage.removeItem('token');
        this._router.navigateByUrl('/Login');
        return false;
      }
      return true;
    }
    return false;
   }

   isArrayEmpty(arr:[]){
    return (arr === undefined || arr.length==0);
   }

   roleMatch(allowedRoles):boolean {
     var token = localStorage.getItem('token');
     var isMatch = false;
     var payLoad = JSON.parse(window.atob(token!.split('.')[1]));
     var userRole = payLoad.role;
    
     if(!this.isArrayEmpty(allowedRoles)){
      allowedRoles.forEach(element => {
        if(userRole == element){
          isMatch = true;
          return false;
        }
       });
     }
     else{
       isMatch = userRole == allowedRoles;
     }  
     return isMatch;
   }

  PostUser(data){
    return this.http.post(this.API_link + '/User/Register',data); // http://localhost:62214/api/User/Register
  }

  LoginUser(data){
    return this.http.post(this.API_link + '/User/Login',data); // http://localhost:62214/api/User/Login
  }

  GetUser(){
    return this.http.get(this.API_link + '/User'); // http://localhost:62214/api/User
  }

  ToggleFavorite(productId:IProductId){
    return this.http.post(this.API_link + '/User/ToggleFavorite',{"ProductId":productId}); // http://localhost:62214/api/User/ToggleFavorite 
  }

  GetFavoritedProducts(){
    return this.http.get(this.API_link + '/User/GetFavoritedProducts'); // http://localhost:62214/api/User/GetFavoritedProducts 
  }

  UpdateUser(data){
    return this.http.patch(this.API_link + '/User/UpdateUser',data); // http://localhost:62214/api/User/UpdateUser
  }

  ForgotPassword(Email){
    return this.http.post(this.API_link + '/User/ForgotPassword',Email); // http://localhost:62214/api/User/ForgotPassword 
  }

  ChangePassword(data){
    return this.http.post(this.API_link + '/User/ChangePassword',data); // http://localhost:62214/api/User/ChangePassword
  }

  ResetPassword(data){
    return this.http.post(this.API_link +'/User/ResetPassword',data); // http://localhost:62214/api/User/ResetPassword
  }

  SendUserTicket(data){
    return this.http.post(this.API_link + '/User/SendUserTicket',data); // http://localhost:62214/api/User/SendUserTicket
  }

  GetTicketByUserId(id:string){
    return this.http.get(this.API_link + '/User/GetTicketByUserId/' + id) // http://localhost:62214/api/User/GetTicketByUserId/{id}
  }

  GetTicketByUser(){
    return this.http.get(this.API_link + '/User/GetTicketByUser'); // http://localhost:62214/api/User/GetTicketByUser
  }

  DeleteTicket(id:string){
    return this.http.delete(this.API_link + '/User/DeleteTicket/'+id); // http://localhost:62214/api/User/DeleteTicket/{id}
  }

  DeleteSelectedTicket(model:IDeleteTicketModel){
    return this.http.post(this.API_link + '/User/DeleteSelectedTicket',model); // http://localhost:62214/api/User/DeleteSelectedTicket
  }

  CloseCurrentTicketByUser(model){
    return this.http.patch(this.API_link + '/User/CloseCurrentTicketByUser',model); // http://localhost:62214/api/User/CloseCurrentTicketByUser
  }

  AddProfilePicture(model){
    return this.http.patch(this.API_link + '/User/AddProfilePicture',model); // http://localhost:62214/api/User/AddProfilePicture
  }

  GetProfileImage(){
    return this.http.get(this.API_link  +'/User/GetProfileImage'); // http://localhost:62214/api/User/GetProfileImage
  }

  GetMessagesByUser(){
    return this.http.get(this.API_link + '/User/GetMessagesByUser'); // http://localhost:62214/api/User/GetMessagesByUser
  }

  GetMessagesByConversationId(id:string){
    return this.http.get(this.API_link + '/User/GetMessagesByConversationId/'+ id); // http://localhost:62214/api/User/GetMessagesByConversationId/{id}
  }

  SendMessage(model){
    return this.http.post(this.API_link + '/User/SendMessage',model); // http://localhost:62214/api/User/SendMessage
  }

  GetMessagesByUserId(id:string){
    return this.http.get(this.API_link + '/User/GetMessagesByUserId/'+ id); // http://localhost:62214/api/User/GetMessagesByUserId
  }

  GetProfileImageByProductId(id:string){
    return this.http.get(this.API_link  +'/User/GetProfileImageByProductId/'+id); // http://localhost:62214/api/User/GetProfileImageByProductId/{id}
  }

  GetProfileImageByUserId(id:string){
    return this.http.get(this.API_link  +'/User/GetProfileImageByUserId/'+id); // http://localhost:62214/api/User/GetProfileImageByUserId/{id}
  }
  GetDetailsByUserId(id:string){
    return this.http.get(this.API_link  +'/User/GetDetailsByUserId/'+id); // http://localhost:62214/api/User/GetDetailsByUserId/{id}
  }

  SendRateAndComment(model){
    return this.http.post(this.API_link + '/User/SendRateAndComment',model); // http://localhost:62214/api/User/SendRateAndComment
  }
  GetAllRatingsByUserId(id:string){
    return this.http.get(this.API_link + '/User/GetAllRatingsByUserId/'+id); // http://localhost:62214/api/User/GetAllRatingsByUser
  }
  CanRate(id){
    return this.http.get(this.API_link + '/User/CanRate/' + id); // http://localhost:62214/api/User/CanRate/{sellerId}
  }

  GetAllUsers(){
    return this.http.get(this.API_link + '/User/GetAllUsers'); // http://localhost:62214/api/User/GetAllUsers
  }
  GetFavoritedProductByProductId(id:number){
    return this.http.get(this.API_link + '/User/GetFavoritedProductByProductId/'+id); // http://localhost:62214/api/User/GetFavoritedProductByProductId/{id}
  }

}
