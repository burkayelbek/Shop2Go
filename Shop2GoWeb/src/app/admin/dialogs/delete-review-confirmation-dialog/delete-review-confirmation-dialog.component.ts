import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { VisitedUserProfileComponent } from 'src/app/user/visited-user-profile/visited-user-profile.component';

@Component({
  selector: 'app-delete-review-confirmation-dialog',
  templateUrl: './delete-review-confirmation-dialog.component.html',
  styleUrls: ['./delete-review-confirmation-dialog.component.css']
})
export class DeleteReviewConfirmationDialogComponent implements OnInit {

  constructor
  (
    public deleteReviewDialogRef: MatDialogRef<VisitedUserProfileComponent>
  ) { }

  ngOnInit(): void {
  }

  onNoClick(event){
    this.deleteReviewDialogRef.close(false);
    event.preventDefault();
  }

}
