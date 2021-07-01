import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IDeleteTicketModel } from './interfaces';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  readonly API_link = 'http://localhost:62214/api'; //Allow communicating route for backend api
  constructor
  (
    private http:HttpClient
  ) { }

  GetAllTickets(){
    return this.http.get(this.API_link + '/Admin/GetAllTickets'); // http://localhost:62214/api/Admin/GetAllTickets
  }

  UpdateTicket(data){
    return this.http.patch(this.API_link + '/Admin/UpdateTicket',data); // http://localhost:62214/api/Admin/UpdateTicket
  }
  DeleteTicketByuserId(id:string){
    return this.http.delete(this.API_link + '/Admin/DeleteTicketByuserId/'+id); // http://localhost:62214/api/Admin/DeleteTicketByuserId/{id}
  }
  DeleteSelectedTicket(model:IDeleteTicketModel){
    return this.http.post(this.API_link + '/Admin/DeleteSelectedTicket',model); // http://localhost:62214/api/Admin/DeleteSelectedTicket
  }

  ApproveProduct(id:number){
    return this.http.patch(this.API_link + '/Admin/ApproveProduct',{'Id':id}); // http://localhost:62214/api/Admin/ApproveProduct
  }

  RejectProduct(data){
    return this.http.patch(this.API_link + '/Admin/RejectProduct',data) // http://localhost:62214/api/Admin/RejectProduct
  }
  RestrictedUser(id:string){
    return this.http.post(this.API_link + '/Admin/RestrictedUser',{"UserID":id}); // http://localhost:62214/api/Admin/RestrictedUser
  }

  DeleteRestrictedUserById(id:string){
    return this.http.delete(this.API_link + '/Admin/DeleteRestrictedUserById/'+id); // http://localhost:62214/api/Admin/DeleteRestrictedUserById/{id}
  }
  SendMailToUser(model){
    return this.http.post(this.API_link + '/Mail/SendMailToUser',model) // http://localhost:62214/api/Mail/SendMailToUser
  }
  DeleteReview(id:number){
    return this.http.delete(this.API_link + '/Admin/DeleteReview/'+id) // http://localhost:62214/api/Admin/DeleteReview/{id}
  }

}
