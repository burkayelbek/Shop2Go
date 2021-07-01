import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { CategoryOperationsDialogComponent } from '../category-operations-dialog/category-operations-dialog.component';

@Component({
  selector: 'app-delete-category-confirmation-dialog',
  templateUrl: './delete-category-confirmation-dialog.component.html',
  styleUrls: ['./delete-category-confirmation-dialog.component.css']
})
export class DeleteCategoryConfirmationDialogComponent implements OnInit {

  constructor
  (
    public deleteCategoryDialog: MatDialogRef<CategoryOperationsDialogComponent>,
  ) { }

  ngOnInit(): void {
  }

  
  onNoClick(event){
    this.deleteCategoryDialog.close();
    event.preventDefault();
    return false;
  }

}
