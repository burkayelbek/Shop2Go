import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../shared/product.service';
import { Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ICategory } from '../../shared/interfaces';
import { CategoryService } from '../../shared/category.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  @ViewChild('fileInput')
  fileInputVariable: ElementRef;
  
  @Output() SendProductToMenu = new EventEmitter<string>();
  srcResult;
  base64Data:string[] = [];
  productsForm;
  category:ICategory[];

  isImagesLabelVisible = false;
  
  constructor(
    private _formBuilder : FormBuilder,
    private _productService : ProductService,
    private _toastrService : ToastrService,
    private _categoryService: CategoryService,
    private _router:Router
  ) { }

  ngOnInit(): void {
    this.productsForm = this._formBuilder.group({
      Title:['',[
        Validators.required,
        Validators.maxLength(75)
      ]],
      Description:['',[ 
        Validators.maxLength(300)
      ]],
      Price:['',[
        Validators.required,
        Validators.maxLength(12)
      ]],
      CategoryId:['',[
        Validators.required
      ]],
    });

    this.GetAllCategory();
  }

  get Title(){
    return this.productsForm.get('Title');
  }

  get Price(){
    return this.productsForm.get('Price');
  }
  get Description(){
    return this.productsForm.get('Description');
  }
  
  reset(event) {
    this.fileInputVariable.nativeElement.value = "";
    this.base64Data=[];
    event.preventDefault();
  }

  GetAllCategory(){
    this._categoryService.GetAllCategory().subscribe(
      res => {
        this.category = res as ICategory[];
      },
      err =>{
        this._toastrService.error('Problem occured while getting categories!','Error');
      }
    );
  }

  PostProduct(value){
    const data = {Title:value.Title, Description:value.Description, CategoryId:value.CategoryId, Price:value.Price, Image:this.base64Data}
    this._productService.PostProduct(data).subscribe(
      res => {
        if(this.productsForm.valid){
          this.SendProductToMenu.emit();
          this._toastrService.success('Product is successfully added!','Success');
          this.productsForm.reset();
          this.fileInputVariable.nativeElement.value="";
          this.base64Data=[];
        }
      },
      err => {
        this._toastrService.error(err?.error?.message ? err?.error?.message :'Problem occured while adding product!','Error')
      }
    )
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

}