import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/product.service';

@Component({
  selector: 'app-product-change-status-dialog',
  templateUrl: './product-change-status-dialog.component.html',
  styleUrls: ['./product-change-status-dialog.component.css']
})
export class ProductChangeStatusDialogComponent implements OnInit {
  recievedRow;
  userNameForm: FormGroup;
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    public openChangeStatusDialog: MatDialogRef<any>,
    private _formBuilder:FormBuilder,
    private _productService:ProductService,
    private _toastrService:ToastrService
  ) {
    console.log(data);
    this.recievedRow = data;
   }

  ngOnInit(): void {
    this.userNameForm = this._formBuilder.group({
      UserName: ['', Validators.required]
    });
  }

  onNoClick(event){
    this.openChangeStatusDialog.close(false);
    event.preventDefault();
  }

  ChangeProductStatusToSold(){
    const data = {Id:this.recievedRow.Id,Title:this.recievedRow.Title,UserName:this.userNameForm.get('UserName')?.value};
    this._productService.ChangeProductStatusToSold(data).subscribe(
    res =>{
      this._toastrService.success('This product successfully sold!','Successful');
    },
    err=>{
      console.log(err);
    });
  }

}
