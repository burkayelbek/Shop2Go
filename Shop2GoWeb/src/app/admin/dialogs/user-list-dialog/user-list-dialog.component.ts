import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserListComponent } from '../../user-list/user-list.component';
import { faIdCard,faLockOpen,faLock,faEnvelope } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/shared/admin.service';
import { RestrictUserDialogComponent } from '../restrict-user-dialog/restrict-user-dialog.component';
import { RemoveRestrictionDialogComponent } from '../remove-restriction-dialog/remove-restriction-dialog.component';
import { SendMailDialogComponent } from '../send-mail-dialog/send-mail-dialog.component';

@Component({
  selector: 'app-user-list-dialog',
  templateUrl: './user-list-dialog.component.html',
  styleUrls: ['./user-list-dialog.component.css']
})
export class UserListDialogComponent implements OnInit {
  recievedData;
  personalInformationIcon = faIdCard;
  restrictIcon = faLock;
  openRestrictionIcon = faLockOpen;
  mailIcon = faEnvelope;

  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    public dialogRef: MatDialogRef<UserListComponent>,
    public _dialog: MatDialog,
    private _toastr:ToastrService,
    private _adminService:AdminService
  ) 
  { 
    this.recievedData = data;
  }

  ngOnInit(): void {
  }

  openRestrictDialog(id:string){
    let openRestrictDialog = this._dialog.open(RestrictUserDialogComponent,{
      autoFocus:false,
      disableClose:false
    });
    openRestrictDialog.afterClosed().subscribe(
    res => {
      if (res) {
        this.RestrictedUser(id);
      }
      openRestrictDialog == null;
    });
  }

  openRemoveRestrictionDialog(id:string){
    let openRestrictDialog = this._dialog.open(RemoveRestrictionDialogComponent,{
      autoFocus:false,
      disableClose:false
    });
    openRestrictDialog.afterClosed().subscribe(
    res => {
      if (res) {
        this.DeleteRestrictedUserById(id);
      }
      openRestrictDialog == null;
    });
  }

  openSendMailDialog(mailValue){
    let openMailDialog = this._dialog.open(SendMailDialogComponent,{
      autoFocus:false,
      disableClose:false,
      data:{mailValue}
    });
    openMailDialog.afterClosed().subscribe(
    res => {
   
    });
  }

  RestrictedUser(id){
    this._adminService.RestrictedUser(id).subscribe(
    res =>{
      this._toastr.warning('User is restricted!');
      this.dialogRef.close();
    },
    err=>{
      console.log(err);
    });
  }
  DeleteRestrictedUserById(id:string){
    this._adminService.DeleteRestrictedUserById(id).subscribe(
    res =>{
      this._toastr.success('User restriction is removed!');
      this.dialogRef.close();
    },
    err=>{
      console.log(err);
    });
  }

}
