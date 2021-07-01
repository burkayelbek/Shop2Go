import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/shared/category.service';
import { ProductManagementComponent } from '../../product-management/product-management.component';
import { CategoryOperationsDialogComponent } from '../category-operations-dialog/category-operations-dialog.component';

@Component({
  selector: 'app-add-category-dialog',
  templateUrl: './add-category-dialog.component.html',
  styleUrls: ['./add-category-dialog.component.css']
})
export class AddCategoryDialogComponent implements OnInit {
  categoryForm:FormGroup
  constructor
  (
    public addCategoryDialog: MatDialogRef<CategoryOperationsDialogComponent>, //For using addCategoryDialog features in this component
    private _formBuilder:FormBuilder,
    private _categoryService:CategoryService,
    private _toastr:ToastrService,
  ) { }

  ngOnInit(): void {
    this.categoryForm = this._formBuilder.group({
      Name:['',[
        Validators.required,
        Validators.maxLength(20)
      ]],
      MatIconName:['',[
        Validators.maxLength(20)
      ]]

    });
  }
  AddCategory(model){
    this._categoryService.AddCategory(model).subscribe(
    res =>{
      this._toastr.success('Category successfully added!','Success');
    },
    err=>{
      this._toastr.error(err?.error?.message ? err?.error?.message :'Problem occured while adding category!','Error');
    });
  }

}
