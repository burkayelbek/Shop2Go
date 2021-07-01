import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import {  IProducts } from 'src/app/shared/interfaces';
import { ProductService } from 'src/app/shared/product.service';
import { UserService } from 'src/app/shared/user.service';
import { faComment } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ProductDeleteConfirmationDialogComponent } from '../dialogs/product-delete-confirmation-dialog/product-delete-confirmation-dialog.component';
import { ChatDialogComponent } from 'src/app/user/dialogs/chat-dialog/chat-dialog.component';
import { EditProductDialogComponent } from '../dialogs/edit-product-dialog/edit-product-dialog.component';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  image:any;
  ProfilePicture;
  product:any;
  productStorage:IProducts[];
  randomProductList;
  userFavoritedProduct;
  userId;
  AverageRating; 
  commentIcon = faComment;
  imageObject: Array<object> = [{

  }]; // Image rendering for slider

  starArr = [
    { Id: 1 },
    { Id: 2,},
    { Id: 3 },
    { Id: 4 },
    { Id: 5 }
  ];
  constructor
  (
    private _productService : ProductService,
    private _userService:UserService,
    private _toastrService: ToastrService,
    private _route: ActivatedRoute,
    private _router:Router,
    public dialog:MatDialog,

  ) { }

  ngOnInit(): void {
    this._route.params.subscribe((res: Params) => {
      let id = +res['id']; // Converts string 'id' to a number
      this.GetProductById(id);
      this.GetProfileImageByProductId(id);
      this.GetAllRatingsByProductId(id);
      this.GetFavoritedProductByProductId(id);
    });

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

   loginCheck(){
    return this._userService.loginCheck();
  }
  roleMatchAdmin(){
    return this._userService.roleMatch(['Admin']);
  }

  GetProfileImageByProductId(id){
    this._userService.GetProfileImageByProductId(id).subscribe(
    (res:any) => {
      this.ProfilePicture = res.ProfilePicture;
    },
    err=>{
    });
  }

  GetProductById(id:number){
    this._productService.GetProductById(id).subscribe(
     (res:any) => {
        this.product = res.products as IProducts;
        this.image = res.image;
        this.randomProductList = res.randomProductList;
        this.imageObject = []; // Push the images to the slider's variable.
        if(this.image.length == 0 ){
          this.imageObject =[
            {image:'assets/images/empty-product.svg',thumbImage:'assets/images/empty-product.svg',alt:'',title:''} // Push the images to the slider.
          ];
        }
        this.image.forEach(element => {
          this.imageObject.push({image:element.Image,thumbImage:element.Image,alt:'',title:''}); // Push the images to the slider.
        });
    },
    err => {
     console.log(err);
    });
  }

  ToggleFavorite(ProductId){
    this._userService.ToggleFavorite(ProductId).subscribe(
     (res:any) => {
      this._toastrService.success(res.message,'Success');
      this.ngOnInit();
    },
     err => {
      this._toastrService.error('You cannot do this operation!','Error')

    });
  }
  DeleteProduct(id){
    this._productService.DeleteProduct(id).subscribe(
      res => {
        this._toastrService.success('Successfully Deleted!','Successful');
        this._router.navigateByUrl('/home');
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
      }
      dialogRef == null;
    });
  }

  openChatDialog(RecieverId){
    let dialogRef = this.dialog.open(ChatDialogComponent,{
      width:'750px',
      data:{RecieverId}
    });
    dialogRef.afterClosed().subscribe(
    res => {
        
    });
  }

  openEditProductDialog(model){
    var openEditDialog = this.dialog.open(EditProductDialogComponent,{
      disableClose:false,
      autoFocus:false,
      data:{Id:model.Id,Title:model.Title,Description:model.Description,CategoryId:model.Category.Id,Price:model.Price}
    });
    openEditDialog.afterClosed().subscribe(
    res => {
      if (res) {
        this.GetProductById(this.product.Id);
      }
      openEditDialog == null;
    });
  }

  GetAllRatingsByProductId(id:number){
    this._productService.GetAllRatingsByProductId(id).subscribe(
    (res:any) =>{
      this.AverageRating = res.AverageRating;
    },
    err=>{
    });
  }
  GetFavoritedProductByProductId(id:number){
    this._userService.GetFavoritedProductByProductId(id).subscribe(
      (res:any)=>{
        this.userFavoritedProduct = res;
      },
      err =>{
      }
    );
  }

  MessageDetailNavigate(id:string){
    this._router.navigate(['/Messages/',id]);
  }

  ProfileDetailNavigate(id:string){
    this._router.navigate(['/User-Profile/',id]);
  }
  ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id]);
  }
  
}