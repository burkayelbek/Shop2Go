import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserListDialogComponent } from '../user-list-dialog/user-list-dialog.component';

@Component({
  selector: 'app-restrict-user-dialog',
  templateUrl: './restrict-user-dialog.component.html',
  styleUrls: ['./restrict-user-dialog.component.css']
})
export class RestrictUserDialogComponent implements OnInit {

  constructor
  (
    public openRestrictDialog: MatDialogRef<UserListDialogComponent>

  ) { }

  ngOnInit(): void {
  }
  onNoClick(event){
    this.openRestrictDialog.close();
    event.preventDefault();
  }

}
