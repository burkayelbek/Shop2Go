import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/product.service';
import { UserService } from 'src/app/shared/user.service';
import { faComment } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { ChatDialogComponent } from 'src/app/user/dialogs/chat-dialog/chat-dialog.component';
import { CategoryService } from 'src/app/shared/category.service';

@Component({
  selector: 'app-get-products-category-name',
  templateUrl: './get-products-category-name.component.html',
  styleUrls: ['./get-products-category-name.component.css']
})
export class GetProductsCategoryNameComponent implements OnInit {
  productsByCategoryName;
  categories;
  userId;
  commentIcon = faComment;
  constructor(
    private _toastrService: ToastrService,
    private _userService:UserService,
    private _productService:ProductService,
    private _categoryService:CategoryService,
    private _route: ActivatedRoute,
    private _router:Router,
    private _dialog:MatDialog
  ) { }

  ngOnInit(): void {
    this._route.params.subscribe((res: Params) => {
      let name = res['name'];
      this.GetProductsByCategoryName(name);
      this.GetCategoryByName(name);
    });
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

  GetCategoryByName(name:string){
    this._categoryService.GetCategoryByName(name).subscribe(
    res => {
      this.categories = res;
      console.log(this.categories);
    },
    err=>{
      this._toastrService.error('Categories could not get successfully!','Error');
      console.error(err);
    });
  }
  

  GetProductsByCategoryName(name:string){
    this._productService.GetProductsByCategoryName(name).subscribe(
    (res:any)=>{
      this.productsByCategoryName = res;
    },
    err=>{
      this._toastrService.error('Products could not get successfully!','Error');
    });
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
