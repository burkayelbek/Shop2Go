import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { VisitedUserProfileComponent } from '../visited-user-profile/visited-user-profile.component';

@Component({
  selector: 'app-review-rating',
  templateUrl: './review-rating.component.html',
  styleUrls: ['./review-rating.component.css']
})
export class ReviewRatingComponent implements OnInit {
  messageForm:FormGroup;
  recievedData;
  private starCount: number = 5;
  selectedRating = 0;
  starArr = [
    { Id: 1,Icon: 'star', Class: 'star gray-star star-hover' },
    { Id: 2,Icon: 'star', Class: 'star gray-star star-hover ' },
    { Id: 3,Icon: 'star', Class: 'star gray-star star-hover ' },
    { Id: 4,Icon: 'star', Class: 'star gray-star star-hover ' },
    { Id: 5,Icon: 'star', Class: 'star gray-star star-hover ' }
  ];
  constructor
  (
    private snackBar: MatSnackBar,
    private _userService:UserService,
    private _formBuilder:FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data:any,
    public reviewDialogRef: MatDialogRef<VisitedUserProfileComponent>,
    private _toastr:ToastrService
  ) { 
    this.recievedData = data;
  }

  ngOnInit(): void {
    this.messageForm = this._formBuilder.group({
      Message:['',[
        
      ]]
    });
   
  }

  onClick(data){
    this.starArr.filter(star => {
      if (star.Id <= data) {
        star.Class = 'star gold-star ';
      }
      else {
        star.Class = 'star gray-star ';
      }
      return star;
    });
    this.selectedRating = +data;
    this.snackBar.open('Rated ' + this.selectedRating + ' / ' + this.starCount, '', {
      duration: 2250
    });
  }

  SendRateAndComment(){
    const data ={RecieverId:this.recievedData.RecieverId,StarRating:this.selectedRating,Comment:this.messageForm.get('Message')?.value};
    this._userService.SendRateAndComment(data).subscribe(
    res => {
      this._toastr.success('Your rating and comment sent successfully!','Success');
      this.messageForm.reset();
      
    },
    err=>{
      this._toastr.error(err?.error?.message ? err?.error?.message :'Your Rating and Comment did not sent','Error');
    });
  }
}