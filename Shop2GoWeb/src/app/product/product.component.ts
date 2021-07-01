import { Component, OnInit } from '@angular/core';
import { faShoppingBasket } from '@fortawesome/free-solid-svg-icons';
import { faTags } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  showForm: boolean = false;
  showFavorites: boolean = false;
  showActiveProducts : boolean = true;
  showPassiveProducts : boolean=false;
  showSoldProducts :boolean=false;
  
  productIcon=faShoppingBasket;
  faTags=faTags;
  
  constructor( ) { }

  ngOnInit(): void {
 
  }

  step = 0;
  setStep(index: number) {
    this.step = index;
  }

  showFormToggle():void{
    this.showForm=true;
    this.showActiveProducts=false;
    this.showFavorites = false;
    this.showPassiveProducts = false;
    this.showSoldProducts = false;
  }
  showActiveProductsToggle():void{
    this.showForm=false;
    this.showActiveProducts=true;
    this.showFavorites = false;
    this.showPassiveProducts = false;
    this.showSoldProducts = false;
  }
  showMyFavoritesToggle():void{
    this.showForm=false;
    this.showActiveProducts=false;
    this.showFavorites = true;
    this.showPassiveProducts = false;
    this.showSoldProducts = false;
  }

  showPassiveProductsToggle():void{
    this.showForm=false;
    this.showActiveProducts=false;
    this.showFavorites = false;
    this.showPassiveProducts = true;
    this.showSoldProducts = false;
  }

  showSoldProductsToggle():void{
    this.showSoldProducts = true;
    this.showForm=false;
    this.showActiveProducts=false;
    this.showFavorites = false;
    this.showPassiveProducts = false;
  }
 
}
