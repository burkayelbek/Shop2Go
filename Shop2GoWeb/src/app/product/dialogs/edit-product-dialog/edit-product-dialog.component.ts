import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timeout } from 'rxjs/operators';
import { CategoryService } from 'src/app/shared/category.service';
import { ICategory } from 'src/app/shared/interfaces';
import { ProductService } from 'src/app/shared/product.service';
import { ProductDetailsComponent } from '../../product-details/product-details.component';

@Component({
  selector: 'app-edit-product-dialog',
  templateUrl: './edit-product-dialog.component.html',
  styleUrls: ['./edit-product-dialog.component.css']
})
export class EditProductDialogComponent implements OnInit {
  productsForm;
  recievedData;
  redirectTime;
  srcResult;
  category;
  base64Data:string[] = [];

  @ViewChild('fileInput')
  fileInputVariable: ElementRef;

  constructor
  (
    @Inject(MAT_DIALOG_DATA) public data:any,
    public openEditDialog: MatDialogRef<any>,
    private _formBuilder : FormBuilder,
    private _productService : ProductService,
    private router:Router,
    private toastr:ToastrService,
    private _categoryService:CategoryService
    
  ) { 
    this.recievedData = data;
  }

  ngOnInit(): void {
    this.productsForm = this._formBuilder.group({
      Title:[this.recievedData.Title,[
        Validators.required
      ]],
      Description:[this.recievedData.Description,[ 
      ]],
      CategoryId:[this.recievedData.CategoryId,[
      ]],
      Price:[this.recievedData.Price,[
        Validators.required,
      ]],
      
    });
    this.GetAllCategory();
  }

  GetAllCategory(){
    this._categoryService.GetAllCategory().subscribe(
      res => {
        this.category = res as ICategory[];
      },
      err =>{
        this.toastr.error('Problem occured while getting categories!','Error');
      }
    );
  }

  UpdateProduct(model){
    const data = {Id:this.recievedData.Id,Title:model.Title, CategoryId:model.CategoryId,Description:model.Description, Price:model.Price, Image:this.base64Data}
    this._productService.UpdateProduct(data).subscribe(
    (res:any)=>{
      this.toastr.success('Edited Successfully! Redirected to the homepage','Success');
       this.redirectTime = setTimeout(()=>{
        this.router.navigateByUrl('/home');
      },2500); 
    },
    err=>{
      this.toastr.error('There was an error while updating the product!','Error');
    });

  }

  handleFile(fileEvent: any, fileInput: any) {
    if (fileInput.length === 0)
      return;
    const inputNode: any = document.querySelector('#file');
    if (typeof (FileReader) !== 'undefined') {
      for(var i =0;i<inputNode.files.length;i++){
        let reader = new FileReader();
        reader.readAsArrayBuffer(inputNode.files[i]);
        reader.onload = (e: any) => {
          this.srcResult = e.target.result;
          this.ConvertArrayBufferToBase64(this.srcResult);
        }
      }
    }
  }
  ConvertArrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    for (var i = 0; i < bytes.byteLength; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    this.base64Data.push(window.btoa(binary));
  }

  reset(event) {
    this.fileInputVariable.nativeElement.value = "";
    this.base64Data=[];
    event.preventDefault();
  }

}
