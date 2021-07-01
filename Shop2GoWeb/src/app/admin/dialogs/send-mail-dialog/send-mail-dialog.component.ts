import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/shared/admin.service';
import { UserListDialogComponent } from '../user-list-dialog/user-list-dialog.component';

@Component({
  selector: 'app-send-mail-dialog',
  templateUrl: './send-mail-dialog.component.html',
  styleUrls: ['./send-mail-dialog.component.css']
})
export class SendMailDialogComponent implements OnInit {
  mailForm:FormGroup;
  recievedMailData;
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    private _formBuilder:FormBuilder,
    private _adminService:AdminService,
    public openMailDialog: MatDialogRef<UserListDialogComponent>,
    private _toastr:ToastrService
  ) {
    this.recievedMailData = data;
   }

  ngOnInit(): void {
    this.mailForm = this._formBuilder.group({
      Id:[this.recievedMailData.mailValue.Id,[]],
      Email:[this.recievedMailData.mailValue.Email,[]],
      Content:['',[
        Validators.required
      ]]
    });
  }
  SendMailToUser(){
    var data = {Id:this.recievedMailData.mailValue.Id,Email:this.recievedMailData.mailValue.Email,Content:this.mailForm.get('Content')?.value};
    this._adminService.SendMailToUser(data).subscribe(
    res=>{
      this._toastr.success('Email sent to the user!','Success');
    },
    err=>{
      this._toastr.success('Email could not sent!','Error');
    });
  }

}
