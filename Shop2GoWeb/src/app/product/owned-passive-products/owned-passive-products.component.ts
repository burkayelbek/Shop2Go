import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/product.service';
import { EditProductDialogComponent } from '../dialogs/edit-product-dialog/edit-product-dialog.component';
import { ProductDeleteConfirmationDialogComponent } from '../dialogs/product-delete-confirmation-dialog/product-delete-confirmation-dialog.component';
import { UserProductRejectedMessageDialogComponent } from '../dialogs/user-product-rejected-message-dialog/user-product-rejected-message-dialog.component';

@Component({
  selector: 'app-owned-passive-products',
  templateUrl: './owned-passive-products.component.html',
  styleUrls: ['./owned-passive-products.component.css']
})
export class OwnedPassiveProductsComponent implements OnInit {
  userPassiveProducts:any;
  constructor
  (
    private _productService:ProductService,
    private _toastrService:ToastrService,
    private _router:Router,
    public dialog:MatDialog,
    private _toastr:ToastrService
  ) { }

  ngOnInit(): void {
    this.GetPassiveProductByUser();
  }
  isArrayEmpty(arr:any[]){
    return (arr === undefined || arr.length==0);
   }

  GetPassiveProductByUser(){
    this._productService.GetPassiveProductByUser().subscribe(
      (res:any) =>{
        this.userPassiveProducts = res;
      },
      err =>{
        if(err.status == 404 || err.status == 400){
          this.userPassiveProducts = [];
        }
        this._toastrService.info('Any product does not exist!','Not Found');
      });
  }

  DeleteProduct(id){
    this._productService.DeleteProduct(id).subscribe(
      res => {
        this.GetPassiveProductByUser();
        this._toastrService.success('Successfully Deleted!','Successful');
      },
      err => {
        this._toastrService.error('Something happened!','Error');
      }
    )
  }

  ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id])
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
        this.GetPassiveProductByUser();
      }
      dialogRef == null;
    });
  }
  showRejectedStatusDialog(model){
    var dialogRef = this.dialog.open(UserProductRejectedMessageDialogComponent,{
      disableClose:false,
      autoFocus:false,
      data:{RejectedMessage:model.product.RejectedMessage}
    });
    dialogRef.afterClosed().subscribe(
    res => {
      if (res) {
        this.GetPassiveProductByUser();
      }
      dialogRef == null;
    });
  }

  openEditProductDialog(model){
    var editFromOwnedDialog = this.dialog.open(EditProductDialogComponent,{
      disableClose:false,
      autoFocus:false,
      data:{Id:model.product.Id,Title:model.product.Title,Description:model.product.Description,CategoryId:model.product.Category.Id,Price:model.product.Price}
    });
    editFromOwnedDialog.afterClosed().subscribe(
    res => {
      if (res) {
        this.GetPassiveProductByUser();
      }
      editFromOwnedDialog == null;
    });
  }

}
