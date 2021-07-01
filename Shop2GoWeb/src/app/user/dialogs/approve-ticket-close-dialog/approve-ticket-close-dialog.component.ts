import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { HelpTicketDetailsDialogComponent } from '../help-ticket-details-dialog/help-ticket-details-dialog.component';

@Component({
  selector: 'app-approve-ticket-close-dialog',
  templateUrl: './approve-ticket-close-dialog.component.html',
  styleUrls: ['./approve-ticket-close-dialog.component.css']
})
export class ApproveTicketCloseDialogComponent implements OnInit {

  constructor(
    public helpTicketDialogRef: MatDialogRef<HelpTicketDetailsDialogComponent>,
    public approveTicketCloseDialogRef: MatDialogRef<ApproveTicketCloseDialogComponent>
  ) { }

  ngOnInit(): void {
  }
  onNoClick(event){
    this.helpTicketDialogRef.close();
    event.preventDefault();
  }


  ApproveTicketClosedStatus(){
    this.helpTicketDialogRef.close(true);
  }
 
}
