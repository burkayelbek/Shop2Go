import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IFavoritedProduct } from 'src/app/shared/interfaces';
import { UserService } from 'src/app/shared/user.service';
import { faComment } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
@Component({
  selector: 'app-favorited-products',
  templateUrl: './favorited-products.component.html',
  styleUrls: ['./favorited-products.component.css']
})
export class FavoritedProductsComponent implements OnInit {
  userProducts:any;
  commentIcon = faComment;
  constructor(
    private _userService:UserService,
    private _toastrService:ToastrService,
    private _router:Router
  )
  { }

  ngOnInit(): void {
    this.GetFavoritedProducts();
  }
  isArrayEmpty(arr:any[]){
    return (arr === undefined || arr.length==0);
   }

  GetFavoritedProducts(){
    this._userService.GetFavoritedProducts().subscribe(
      (res:any)=>{
         this.userProducts = res;
      },
      err =>{
        if(err.status == 404){
          this.userProducts = [];
        }
        this._toastrService.info('Any favorited product does not exist!','Not Found');
      }
    );
  }

  ToggleFavorite(ProductId){
    this._userService.ToggleFavorite(ProductId).subscribe(
     (res:any) => {
       this._toastrService.success(res.message,'Success');
       this.GetFavoritedProducts();
    },
     err => {
       this._toastrService.error('You cannot do this operation!','Error')

    });
  }

  ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id]);
  }

  MessageDetailNavigate(id:string){
    this._router.navigate(['/Messages/',id]);
  }

}
