import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { MyTicketsComponent } from '../../my-tickets/my-tickets.component';
import { ApproveTicketCloseDialogComponent } from '../approve-ticket-close-dialog/approve-ticket-close-dialog.component';

@Component({
  selector: 'app-help-ticket-details-dialog',
  templateUrl: './help-ticket-details-dialog.component.html',
  styleUrls: ['./help-ticket-details-dialog.component.css']
})
export class HelpTicketDetailsDialogComponent implements OnInit {
  ticketDetailsForm;
  recievedRow;

  pipe = new DatePipe('en-US');
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    private _formBuilder:FormBuilder,
    private _userService:UserService,
    private _toastrService:ToastrService,
    public _dialog: MatDialog,
    public ticketdialogRef: MatDialogRef<MyTicketsComponent>
  ) {
    this.recievedRow = data;
   }

  ngOnInit(): void {
    this.ticketDetailsForm = this._formBuilder.group({
      Id:[this.recievedRow.value.Id,[]],
      UserFullName:[{value:this.recievedRow.value.UserFullName,disabled:true},[
      ]],
      Title:[{value:this.recievedRow.value.Title,disabled:true},[
      ]],
      Message:[{value:this.recievedRow.value.Message,disabled:true},[
      ]],
      Priority:[this.recievedRow.value.Priority,[
      ]],
      Status:[{value:this.recievedRow.value.Status==0?'Open':'Closed',disabled:true},[
      ]],
      CreatedOn:[{value:this.pipe.transform(this.recievedRow.value.CreatedOn,'dd/MM/yyyy HH:mm'),disabled:true},[
      ]],
      FeedbackMessage:[{value:this.recievedRow.value.FeedbackMessage?this.recievedRow.value.FeedbackMessage:'',disabled:true},[
      ]],
    });
  }

  onNoClick(event){
    this.ticketdialogRef.close();
    event.preventDefault();
    return false;
  }

  openTicketClosedStatus():any{
    let dialogRef = this._dialog.open(ApproveTicketCloseDialogComponent,{
      disableClose:false,
      autoFocus:false
      
    });
    dialogRef.afterClosed().subscribe(
    res => {
      if (res) {
        this.CloseCurrentTicketByUser();
        this.ticketdialogRef.close();
      }
    });
  }

  get Id(){
    return this.ticketDetailsForm.get("Id").value;
  }
  get Status(){
    return this.ticketDetailsForm.get("Status").value;
  }

  CloseCurrentTicketByUser(){
    const data = {Id:this.Id,Status:this.Status};
    this._userService.CloseCurrentTicketByUser(data).subscribe(
    res => {
      this._toastrService.success('Your ticket closed successfully!','Success');
    },
    err=> {
      if(err.status == 404 || err.status == 400){
        this.data = [];
      }
    });
  }

}
