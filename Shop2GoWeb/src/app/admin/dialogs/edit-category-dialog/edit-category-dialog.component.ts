import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/shared/category.service';
import { CategoryOperationsDialogComponent } from '../category-operations-dialog/category-operations-dialog.component';

@Component({
  selector: 'app-edit-category-dialog',
  templateUrl: './edit-category-dialog.component.html',
  styleUrls: ['./edit-category-dialog.component.css']
})
export class EditCategoryDialogComponent implements OnInit {
  categoryEditForm:FormGroup;
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    private _formBuilder:FormBuilder,
    private _toastrService:ToastrService,
    private _categoryService:CategoryService,
    public _dialog: MatDialog,
    public editCategoryDialog: MatDialogRef<CategoryOperationsDialogComponent>
    
  ) {
    console.log(data);
   }

  ngOnInit(): void {
    this.categoryEditForm = this._formBuilder.group({
      Id:[this.data.value.Id,[
      ]],
      Name:[this.data.value.Name,[ 
        Validators.maxLength(20)
      ]],
      MatIconName:[this.data.value.MatIconName,[
        Validators.required,
        Validators.maxLength(20)
      ]],
    });
  }
  onNoClick(event){
    this.editCategoryDialog.close();
    event.preventDefault();
    return false;
  }

  UpdateCategory(model){
    this._categoryService.EditCategory(model).subscribe(
    res=>{
      this._toastrService.success('Category updated successfully!','Success');
    },
    err=>{
      this._toastrService.error(err?.error?.message ? err?.error?.message :'A problem occured while updating category!','Error');
    });
  }


}
