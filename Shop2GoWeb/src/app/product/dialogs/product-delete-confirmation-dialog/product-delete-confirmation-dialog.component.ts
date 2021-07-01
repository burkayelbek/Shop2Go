import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OwnedPassiveProductsComponent } from '../../owned-passive-products/owned-passive-products.component';

@Component({
  selector: 'app-product-delete-confirmation-dialog',
  templateUrl: './product-delete-confirmation-dialog.component.html',
  styleUrls: ['./product-delete-confirmation-dialog.component.css']
})
export class ProductDeleteConfirmationDialogComponent implements OnInit {
  constructor
  (
    public dialogRef: MatDialogRef<any>

  ) {  }

  ngOnInit(): void {
  }

  onNoClick(event){
    this.dialogRef.close(false);
    event.preventDefault();
  }

}
