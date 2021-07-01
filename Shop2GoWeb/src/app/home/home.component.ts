import {  Component, OnInit } from '@angular/core';
import { ProductService } from '../shared/product.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/user.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { faComment, faMedkit,faCar, faTshirt } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ChatDialogComponent } from '../user/dialogs/chat-dialog/chat-dialog.component';
import { CategoryService } from '../shared/category.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  products:any[];
  category:any;
  userId;
  commentIcon=faComment;
  carIcon = faCar;
  healthIcon = faMedkit;
  clothIcon = faTshirt;
  imageObject: Array<object> = [
    {
      image: 'assets/images/slider-photo-1.png',
      thumbImage: 'assets/images/slider-photo-1.png',
      alt: '',
      title:''
    },
    {
      image: 'assets/images/slider-photo-2.png',
      thumbImage: 'assets/images/slider-photo-2.png',
      alt: '',
      title:''
    }];

  toUserId;
  constructor
  (
    private _productService : ProductService,
    private _toastrService: ToastrService,
    private _categoryService:CategoryService,
    private _userService:UserService,
    private _router:Router,
    public _dialog:MatDialog

  ) { }

  ngOnInit(): void {
    this.GetActiveProducts();
    this.GetAllCategories();
  }

  GetActiveProducts(){
    this._productService.GetActiveProducts().subscribe(
      (res:any) => {
        this.products = res;
      },
      err =>{
        console.log(err);
      }
    );
  }

  GetAllCategories(){
    this._categoryService.GetAllCategory().subscribe(
    res => {
      this.category = res;
    },
    err=>{

    });
  }

  DeleteProduct(id){
    this._productService.DeleteProduct(id).subscribe(
      res => {
        this.GetActiveProducts();
        this._toastrService.success('Product successfully Deleted!','Successful');
      },
      err => {
        this._toastrService.error('Product could not be deleted!','Error');
      }
    )
  }

  loginCheck(){
      return this._userService.loginCheck();
  }
  roleMatchAdmin(){
    return this._userService.roleMatch(['Admin']);
  }

  checkUserId(USERID):boolean{
    const token = localStorage.getItem('token') as string | undefined;
    const helper = new JwtHelperService();
    this.userId = helper.decodeToken(token).UserID;
    if(this.userId == USERID){
      return true;
    }
    return false;
   }

   ToggleFavorite(ProductId){
     this._userService.ToggleFavorite(ProductId).subscribe(
      (res:any) => {
        this._toastrService.success(res.message,'Success');
     },
      err => {
        this._toastrService.error('You cannot do this operation!','Error')
     });
   }

   ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id]);
    }

  openChatDialog(RecieverId){
    let dialogRef = this._dialog.open(ChatDialogComponent,{
      width:'750px',
      data:{RecieverId}
    });
    dialogRef.afterClosed().subscribe(
    res => {
        
    });
  }
  
}