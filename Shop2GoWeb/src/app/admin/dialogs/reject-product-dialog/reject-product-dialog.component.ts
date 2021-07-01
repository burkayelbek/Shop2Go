import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/shared/admin.service';
import { ProductManagementComponent } from '../../product-management/product-management.component';

@Component({
  selector: 'app-reject-product-dialog',
  templateUrl: './reject-product-dialog.component.html',
  styleUrls: ['./reject-product-dialog.component.css']
})
export class RejectProductDialogComponent implements OnInit {
  recievedData;
  rejectedForm;
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    private _formBuilder:FormBuilder,
    public dialogRef: MatDialogRef<ProductManagementComponent>,
    private _adminService:AdminService,
    private _toastr:ToastrService
    
  ) {
    this.recievedData = data;
   }

  ngOnInit(): void {
    this.rejectedForm = this._formBuilder.group({
      Id:[this.recievedData.value.Id,[
      ]],
      RejectedMessage:[this.recievedData.value.RejectedMessage?this.recievedData.value.RejectedMessage:'',[
      ]]
    });

    console.log('Recieved Data : ',this.recievedData);
  }

  RejectProduct(data){
    this._adminService.RejectProduct(data).subscribe(
      res => {
        this._toastr.success('Product is Rejected!','Success');
      },
      err=>{
        console.log(err);
      });
  }

  onNoClick(event){
    this.dialogRef.close(false);
    event.preventDefault();
    return false;
  }

}
