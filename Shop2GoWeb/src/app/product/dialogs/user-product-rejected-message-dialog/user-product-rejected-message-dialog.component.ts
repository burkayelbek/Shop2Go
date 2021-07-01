import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OwnedPassiveProductsComponent } from '../../owned-passive-products/owned-passive-products.component';

@Component({
  selector: 'app-user-product-rejected-message-dialog',
  templateUrl: './user-product-rejected-message-dialog.component.html',
  styleUrls: ['./user-product-rejected-message-dialog.component.css']
})
export class UserProductRejectedMessageDialogComponent implements OnInit {
  recievedData;
  rejectedStatusForm:FormGroup;
  
  constructor
  (
    public dialogRef: MatDialogRef<OwnedPassiveProductsComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private _formBuilder:FormBuilder
  ) 
  {
    this.recievedData = data;
  }

  ngOnInit(): void {
    this.rejectedStatusForm = this._formBuilder.group({
      RejectedMessage:[{value:this.recievedData.RejectedMessage,disabled:true},[
      ]],
    });
  }

  onNoClick(event){
    this.dialogRef.close(false);
    event.preventDefault();
  }

}
