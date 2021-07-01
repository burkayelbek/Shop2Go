import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/shared/admin.service';
import { UserService } from 'src/app/shared/user.service';
import { AdminHelpTicketComponent } from '../../admin-help-ticket/admin-help-ticket.component';

@Component({
  selector: 'app-admin-help-ticket-details-dialog',
  templateUrl: './admin-help-ticket-details-dialog.component.html',
  styleUrls: ['./admin-help-ticket-details-dialog.component.css']
})
export class AdminHelpTicketDetailsDialogComponent implements OnInit {
  ticketDetailsForm;
  recievedRow;
  pipe = new DatePipe('en-US');
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    private _formBuilder:FormBuilder,
    private _adminService:AdminService,
    private _toastrService:ToastrService,
    public _dialog: MatDialog,
    public dialogRef: MatDialogRef<AdminHelpTicketComponent>
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
      Status:[this.recievedRow.value.Status==0?'Open':'Closed',[
      ]],
      CreatedOn:[{value:this.pipe.transform(this.recievedRow.value.CreatedOn,'dd/MM/yyyy HH:mm'),disabled:true},[
      ]],
      FeedbackMessage:[this.recievedRow.value.FeedbackMessage?this.recievedRow.value.FeedbackMessage:'',[
      ]],
    });

    console.log('Recieved ROW :' ,this.recievedRow);
  }

  UpdateTicket(id:number){
    this._adminService.UpdateTicket(id).subscribe(
      res=>{
        this._toastrService.success('Feedback sent successfully!','Success');
        this.dialogRef.close();
      },
      err=>{
        //this._toastrService.error(err.message,'Error');
        console.log(err);
      });
  }

  onNoClick(event){
    this.dialogRef.close();
    event.preventDefault();
    return false;
  }

}
