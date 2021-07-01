import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  readonly API_link = 'http://localhost:62214/api'; //Allow communicating route for backend api
  constructor(private http:HttpClient) { }

  GetAllCategory(){
    return this.http.get(this.API_link + '/Category');  // http://localhost:62214/api/Category
  }
  AddCategory(model){
    return this.http.post(this.API_link + '/Category',model);  // http://localhost:62214/api/Category
  }

  GetCategoryByName(name:string){
    return this.http.get(this.API_link + '/Category/'+name); // http://localhost:62214/api/Category/{name}
  }

  EditCategory(model){
    return this.http.patch(this.API_link + '/Category',model);  // http://localhost:62214/api/Category
  } 

  DeleteCategory(id:number){
    return this.http.delete(this.API_link +'/Category/'+id) // http://localhost:62214/api/Category/{id}
  }
}
