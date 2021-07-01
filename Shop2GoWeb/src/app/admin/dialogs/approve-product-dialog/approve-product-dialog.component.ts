import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ProductManagementComponent } from '../../product-management/product-management.component';

@Component({
  selector: 'app-approve-product-dialog',
  templateUrl: './approve-product-dialog.component.html',
  styleUrls: ['./approve-product-dialog.component.css']
})
export class ApproveProductDialogComponent implements OnInit {
  constructor
  (
    public dialogRef: MatDialogRef<ProductManagementComponent>
   ) { }

  ngOnInit(): void {
  }
  onNoClick(event){
    this.dialogRef.close(false);
    event.preventDefault();
  }

}
