import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/shared/admin.service';
import { IProducts} from 'src/app/shared/interfaces';
import { ProductService } from 'src/app/shared/product.service';
import { MatDialog } from '@angular/material/dialog';
import { ApproveProductDialogComponent } from '../dialogs/approve-product-dialog/approve-product-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { RejectProductDialogComponent } from '../dialogs/reject-product-dialog/reject-product-dialog.component';
import { AddCategoryDialogComponent } from '../dialogs/add-category-dialog/add-category-dialog.component';
import { CategoryOperationsDialogComponent } from '../dialogs/category-operations-dialog/category-operations-dialog.component';

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.css']
})
export class ProductManagementComponent implements OnInit {
  toggleShowStatus = true;
  passiveProductsStorage:any[];

  passiveProducts:any[];
  constructor
  (
    private _adminService:AdminService,
    private _productService:ProductService,
    private _router:Router,
    public _dialog:MatDialog,
    private _toastr:ToastrService
  ) { }

  ngOnInit(): void {
    this.GetPassiveProducts();
  }

  GetPassiveProducts(){
    this.passiveProducts=[];
    this._productService.GetPassiveProducts().subscribe(
      (res:any) => {
        this.passiveProductsStorage = res;
        res.forEach(element => {
          if (this.toggleShowStatus && element.product.isApproved == 0) {
            this.passiveProducts.push(element);
          }
          else if (!this.toggleShowStatus && element.product.isApproved == 2) {
            this.passiveProducts.push(element);
          }
        });
      },
      err =>{
        if(err.status == 400 || err.status == 404){
          this.passiveProducts = [];
        }
        console.log(err);
      });
  }

  ToggleShowStatus(){
    this.toggleShowStatus = !this.toggleShowStatus;
    this.passiveProducts=[];

    this.passiveProductsStorage.forEach(element =>{
      if (this.toggleShowStatus && element.product.isApproved == 0) {
        this.passiveProducts.push(element);
      }
      else if (!this.toggleShowStatus && element.product.isApproved == 2) {
        this.passiveProducts.push(element);
      }
    });
  }

  ApproveProduct(id:number){
    this._adminService.ApproveProduct(id).subscribe(
      res=>{
        this.GetPassiveProducts();
      },
      err=>{
        console.log(err);
      });
  }

  openApproveDetailsDialog(id:number){
    let dialogRef = this._dialog.open(ApproveProductDialogComponent,{
      disableClose:false,
      autoFocus:false,
    });
    dialogRef.afterClosed().subscribe(
    res => {
      if (res) {
        this.ApproveProduct(id);
        this._toastr.success('Product is approved!','Success');
      }
      dialogRef == null;
    });
  }

  openRejectDetailsDialog(value){
    let dialogRef = this._dialog.open(RejectProductDialogComponent,{
      disableClose:false,
      data:{value}
    });
    dialogRef.afterClosed().subscribe(
    res => {
      if(res){
        this.GetPassiveProducts();
      }
      dialogRef == null;
    });
  }

  ProductDetailNavigate(id:number){
    this._router.navigate(['/Product-Details/',id]);
  }

 
  openCategoryOperationsDialog(){
    let openCategoryOperationDialog = this._dialog.open(CategoryOperationsDialogComponent,{
      disableClose:false,
      autoFocus:false,
      width:'600px',
      maxWidth:'750px'
    });
    openCategoryOperationDialog.afterClosed().subscribe(
    res => {
      if(res){
        this.GetPassiveProducts();
      }
      openCategoryOperationDialog == null;
    });
  }
}
