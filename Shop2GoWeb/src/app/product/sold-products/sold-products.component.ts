import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/product.service';
import { ProductDeleteConfirmationDialogComponent } from '../dialogs/product-delete-confirmation-dialog/product-delete-confirmation-dialog.component';

@Component({
  selector: 'app-sold-products',
  templateUrl: './sold-products.component.html',
  styleUrls: ['./sold-products.component.css']
})
export class SoldProductsComponent implements OnInit {
  soldProducts;
  constructor
  (
    private _productService:ProductService,
    private _toastrService:ToastrService,
    private _router:Router,
    public dialog:MatDialog,
  ) { }

  ngOnInit(): void {
    this.GetSoldProductsByUser();
  }

  isArrayEmpty(arr:any[]){
    return (arr === undefined || arr.length==0);
   }


   GetSoldProductsByUser(){
    this._productService.GetSoldProductsByUser().subscribe(
      res =>{
        this.soldProducts = res;
      },
      err =>{
        if(err.status == 404 || err.status == 400 ){
          this.soldProducts = [];
        }
        console.log(err);
        this._toastrService.info('Any sold does not exist!','Not Found');
      });
  }

  DeleteProduct(id){
    this._productService.DeleteProduct(id).subscribe(
      res => {
        this.GetSoldProductsByUser();
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
        this.GetSoldProductsByUser();
      }
      dialogRef == null;
    },
    err=>{
      if(err.status == 400 || err.status == 404){
        this.soldProducts = [];
      }
    });
  }

  ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id]);
  }

}
