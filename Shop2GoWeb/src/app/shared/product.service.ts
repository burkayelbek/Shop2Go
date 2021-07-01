import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  readonly API_link = 'http://localhost:62214/api'; //Allow communicating route for backend api
  constructor(private http:HttpClient) { }

  GetActiveProducts(){
    return this.http.get(this.API_link + '/Products/GetActiveProducts'); // http://localhost:62214/api/Products/GetActiveProducts
  }

  GetPassiveProducts(){
    return this.http.get(this.API_link + '/Products/GetPassiveProducts'); // http://localhost:62214/api/Products/GetActiveProducts
  }

  PostProduct(data){
    return this.http.post(this.API_link + '/Products',data); // http://localhost:62214/api/Products/PostProduct
  }

  DeleteProduct(id){
    return this.http.delete(this.API_link + '/Products/'+id); // http://localhost:62214/api/Products/DeleteProduct/{id}
  }

  GetProductById(id:number){
    return this.http.get(this.API_link + '/Products/' + id); // http://localhost:62214/api/Products/GetProductById/{id}
  }
  GetActiveProductByUser(){
    return this.http.get(this.API_link + '/Products/GetActiveProductByUser'); // http://localhost:62214/api/Products/GetActiveProductByUser
  }
  GetPassiveProductByUser(){
    return this.http.get(this.API_link + '/Products/GetPassiveProductByUser');  // http://localhost:62214/api/Products/GetPassiveProductByUser
  }
  UpdateProduct(model){
    return this.http.post(this.API_link + '/Products/UpdateProduct',model); // http://localhost:62214/api/Products/UpdateProduct
  }
  GetActiveProductByUserId(id:string){
    return this.http.get(this.API_link + '/Products/GetActiveProductByUserId/'+id); // http://localhost:62214/api/Products/GetProductByUserId/{id}
  }
  GetSoldProductsByUser(){
    return this.http.get(this.API_link + '/Products/GetSoldProductsByUser'); // http://localhost:62214/api/Products/GetSoldProductsByUser
  }
  ChangeProductStatusToSold(model){
    return this.http.post(this.API_link + '/Products/ChangeProductStatusToSold',model);   // http://localhost:62214/api/Products/ChangeProductStatusToSold
  }
  GetAllRatingsByProductId(id:number){
    return this.http.get(this.API_link + '/Products/GetAllRatingsByProductId/'+id); // http://localhost:62214/api/Products/GetUserIdFromProductId
  }

  GetProductsByCategoryName(name:string){
    return this.http.get(this.API_link + '/Products/GetProductsByCategoryName/'+name); // http://localhost:62214/api/Products/GetProductsByCategoryName/{name}
  }

}