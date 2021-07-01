import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/product.service';
import { faComment } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { ProductDeleteConfirmationDialogComponent } from '../dialogs/product-delete-confirmation-dialog/product-delete-confirmation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { EditProductDialogComponent } from '../dialogs/edit-product-dialog/edit-product-dialog.component';
import { ProductChangeStatusDialogComponent } from '../dialogs/product-change-status-dialog/product-change-status-dialog.component';

@Component({
  selector: 'app-owned-products',
  templateUrl: './owned-products.component.html',
  styleUrls: ['./owned-products.component.css']
})
export class OwnedProductsComponent implements OnInit {
  userProducts:any;
  commentIcon=faComment;
  constructor(
    private _productService:ProductService,
    private _toastrService:ToastrService,
    private _router:Router,
    public dialog:MatDialog,
  ) { }

  ngOnInit(): void {
    this.GetActiveProductByUser();
  }

  isArrayEmpty(arr:any[]){
    return (arr === undefined || arr.length==0);
   }


  GetActiveProductByUser(){
    this._productService.GetActiveProductByUser().subscribe(
      (res:any) =>{
        this.userProducts = res;
      },
      err =>{
        if(err.status == 404){
          this.userProducts = [];
        }
        this._toastrService.info('Any product does not exist!','Not Found');
      });
  }

  DeleteProduct(id){
    this._productService.DeleteProduct(id).subscribe(
      res => {
        this.GetActiveProductByUser();
        this._toastrService.success('Successfully Deleted!','Successful');
      },
      err => {
        this._toastrService.error('Something happened!','Error');
      }
    )
  }

  ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id]);
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
        this.GetActiveProductByUser();
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
        this.GetActiveProductByUser();
      }
      editFromOwnedDialog == null;
    });
  }
 
  openProductChangeStatusDialog(model){
    var openChangeStatusDialog = this.dialog.open(ProductChangeStatusDialogComponent,{
      disableClose:false,
      width:'320px',
      autoFocus:false,
      data:{Id:model.product.Id,Title:model.product.Title}
    });
    openChangeStatusDialog.afterClosed().subscribe(
    res => {
      if (res) {
        this.GetActiveProductByUser();
      }
      openChangeStatusDialog == null;
    });
  }

}
