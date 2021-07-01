import {  Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { ProductDeleteConfirmationDialogComponent } from 'src/app/product/dialogs/product-delete-confirmation-dialog/product-delete-confirmation-dialog.component';
import { ProductService } from 'src/app/shared/product.service';
import { UserService } from 'src/app/shared/user.service';
import { faComment } from '@fortawesome/free-solid-svg-icons';
import { EditProductDialogComponent } from 'src/app/product/dialogs/edit-product-dialog/edit-product-dialog.component';
import { ReviewRatingComponent } from '../review-rating/review-rating.component';
import { MatTabChangeEvent, MatTabGroup } from '@angular/material/tabs';
import { AdminService } from 'src/app/shared/admin.service';
import { DeleteReviewConfirmationDialogComponent } from 'src/app/admin/dialogs/delete-review-confirmation-dialog/delete-review-confirmation-dialog.component';

@Component({
  selector: 'app-visited-user-profile',
  templateUrl: './visited-user-profile.component.html',
  styleUrls: ['./visited-user-profile.component.css']
})
export class VisitedUserProfileComponent implements OnInit {
  userProducts:any;
  ProfilePicture;
  userDetails:any;
  reviewComments:any;
  countReviews;
  userInformationForm;
  userId;
  commentIcon = faComment;
  AverageRating = 0;
  canRate = false;

  @ViewChild(MatTabGroup) tabGroup: MatTabGroup;

  starArr = [
    { Id: 1 },
    { Id: 2,},
    { Id: 3 },
    { Id: 4 },
    { Id: 5 }
  ];


  constructor
  (
    private _userService:UserService,
    private _formBuilder:FormBuilder,
    private _route: ActivatedRoute,
    private _productService:ProductService,
    private _adminService:AdminService,
    private _router:Router,
    private _toastrService:ToastrService,
    private dialog:MatDialog

  ) { }

  ngOnInit(): void {
    this._route.params.subscribe((res: Params) => {
      let id = res['id']; 
      this.ProfilePicture="";
      this.GetProfileImageByUserId(id);
      this.GetActiveProductByUserId(id);
      this.GetDetailsByUserId(id);
      this.GetAllRatingsByUserId(id);
      this.GetCanRate(id);
    });
  }

  GetCanRate(id){
    this._userService.CanRate(id).subscribe(
      res => {
        this.canRate = res as boolean;
      },
      err => {
        this._toastrService.error('Could not get data for "CanRate"', 'Error');
        console.warn(err);
      }
    );
  }


  checkUserId(USERID):boolean{
    const token = localStorage.getItem('token') as string | undefined;
    const helper = new JwtHelperService();
    this.userId = helper.decodeToken(token).UserID;
    if(this.userId == USERID ){
      return true;
    }
    return false;
   }

   checkUserIdWithoutParameter():any{
    const token = localStorage.getItem('token') as string | undefined;
    const helper = new JwtHelperService();
    this.userId = helper.decodeToken(token).UserID;
    var tempId;
    this._route.params.subscribe((res: Params) => {
      tempId= res['id']; 
    });
    if(this.userId == tempId ){
      return true;
    }
    else
      return false;
   }

  createForm(){
    this.userInformationForm  = this._formBuilder.group({
      FirstName:['',[]],
      LastName:['',[]],
    });
  }

  isArrayEmpty(arr:any[]){
    return (arr === undefined || arr.length==0);
   }

  GetProfileImageByUserId(id){
    this._userService.GetProfileImageByUserId(id).subscribe(
    (res:any) => {
      this.ProfilePicture = res.ProfilePicture?res.ProfilePicture:'assets/images/user.svg';
    },
    err=>{
    });
  }

  GetActiveProductByUserId(id){
    this._productService.GetActiveProductByUserId(id).subscribe(
    (res:any) => {
      this.userProducts = res;
    },
    err=>{
      console.log(err);
    });

  }

  ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id]);
  }

  roleMatchAdmin(){
    return this._userService.roleMatch(['Admin']);
  }

  DeleteProduct(id){
    this._productService.DeleteProduct(id).subscribe(
      res => {
        this._toastrService.success('Successfully Deleted!','Successful');
      },
      err => {
        this._toastrService.error('Something happened!','Error');
      }
    )
  }
  openDeleteProductDialog(id:number){
    var dialogRef = this.dialog.open(ProductDeleteConfirmationDialogComponent,{
      disableClose:false,
      autoFocus:false
    });
    dialogRef.afterClosed().subscribe(
    res => {
      if (res) {
        this.DeleteProduct(id);
        this.GetActiveProductByUserId(id);
      }
      dialogRef == null;
    });
  }

  GetDetailsByUserId(id:string){
    this._userService.GetDetailsByUserId(id).subscribe(
    res => {
      this.userDetails = res;
    },
    err=>{
      console.log(err);
    });
  }

  MessageDetailNavigate(id:string){
    this._router.navigate(['/Messages/',id]);
  }

  openEditProductDialog(model){
    var editFromOwnedDialog = this.dialog.open(EditProductDialogComponent,{
      disableClose:false,
      autoFocus:false,
      data:{Id:model.tempProduct.Id,Title:model.tempProduct.Title,Description:model.tempProduct.Description,Price:model.tempProduct.Price}
    });
    editFromOwnedDialog.afterClosed().subscribe(
    res => {
      if (res) {
        this.GetActiveProductByUserId(model.tempProduct.UserID);
      }
      editFromOwnedDialog == null;
    });
  }

  openReviewDialog(RecieverId){
    var reviewDialogRef = this.dialog.open(ReviewRatingComponent,{
      disableClose:false,
      autoFocus:false,
      minWidth:'320px',
      minHeight:'320px',
      data:{RecieverId:RecieverId}
    });
    reviewDialogRef.afterClosed().subscribe(
    res => {
      if (res) {
        this.GetActiveProductByUserId(RecieverId);
        this.GetAllRatingsByUserId(RecieverId);
        this.GetCanRate(RecieverId);
        this.ngOnInit();
      }
      reviewDialogRef == null;
    });
  }

  GetAllRatingsByUserId(id:string){
    this._userService.GetAllRatingsByUserId(id).subscribe(
    (res:any) =>{
      this.reviewComments = res.Reviews; 
      this.AverageRating = res.AverageRating;
      this.countReviews = res.CountReviews;
    },
    err=>{
    });
  }

  tabClick(matTabEvent:MatTabChangeEvent){
    var tempId;
    this._route.params.subscribe((res: Params) => {
      tempId= res['id']; 
    });
    if(matTabEvent.tab.textLabel=="Listings"){
     
      this.GetActiveProductByUserId(tempId);
    }
    else if(matTabEvent.tab.textLabel=="Reviews"){
      this.GetAllRatingsByUserId(tempId)
    }
  }
  ProfileDetailNavigate(id:string){
    this._router.navigate(['/User-Profile/',id]).then(() => {
      window.location.reload();
    });
  }

  DeleteReview(id:number){
    this._adminService.DeleteReview(id).subscribe(
    res =>{
      this._toastrService.success('User\'s review removed successfully!','Success');
      this.ngOnInit();
    },
    err=>{
      this._toastrService.error('User\'s review cannot removed!','Error');
    });
  }
  openDeleteReviewDialog(id:number){
    var deleteReviewDialogRef = this.dialog.open(DeleteReviewConfirmationDialogComponent,{
      disableClose:false,
      autoFocus:false,
    });
    deleteReviewDialogRef.afterClosed().subscribe(
    res => {
      if (res) {
        this.DeleteReview(id);
      }
      deleteReviewDialogRef == null;
    });
  }
  
}