import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserListDialogComponent } from '../user-list-dialog/user-list-dialog.component';

@Component({
  selector: 'app-remove-restriction-dialog',
  templateUrl: './remove-restriction-dialog.component.html',
  styleUrls: ['./remove-restriction-dialog.component.css']
})
export class RemoveRestrictionDialogComponent implements OnInit {

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
