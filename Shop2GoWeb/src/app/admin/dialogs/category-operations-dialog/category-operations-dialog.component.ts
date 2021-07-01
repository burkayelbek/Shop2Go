import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/shared/category.service';
import { IGetAllCategories } from 'src/app/shared/interfaces';
import { ProductManagementComponent } from '../../product-management/product-management.component';
import { AddCategoryDialogComponent } from '../add-category-dialog/add-category-dialog.component';
import { DeleteCategoryConfirmationDialogComponent } from '../delete-category-confirmation-dialog/delete-category-confirmation-dialog.component';
import { EditCategoryDialogComponent } from '../edit-category-dialog/edit-category-dialog.component';

@Component({
  selector: 'app-category-operations-dialog',
  templateUrl: './category-operations-dialog.component.html',
  styleUrls: ['./category-operations-dialog.component.css']
})
export class CategoryOperationsDialogComponent implements OnInit,AfterViewInit {
  categories:IGetAllCategories[]=[];
  displayedColumns: string[] = ['Name','MatIconName','Details&Operations'];
  dataSource = new MatTableDataSource(this.categories);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private _categoryService:CategoryService,
    private _toastr:ToastrService,
    private _dialog:MatDialog,
    public openCategoryOperationDialog: MatDialogRef<ProductManagementComponent>,

  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.GetAllCategories();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  GetAllCategories(){
    this._categoryService.GetAllCategory().subscribe(
    res =>{
      this.categories = res as IGetAllCategories[];
      this.dataSource.data = this.categories;
    },
    err=>{
      this._toastr.error('Categories could not loaded!','Error');
    });
  }

  DeleteCategory(id:number){
    this._categoryService.DeleteCategory(id).subscribe(
    res=>{
      this._toastr.success('Categor deleted successfully!','Success');
      this.GetAllCategories();
    },
    err=>{
      this._toastr.error('Category could not deleted!','Error');
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openAddCategoryDialog(){
    let addCategoryDialog = this._dialog.open(AddCategoryDialogComponent,{
      disableClose:false,
      autoFocus:false,
    });
    addCategoryDialog.afterClosed().subscribe(
    res => {
      if(res){
        this.GetAllCategories();
      }
      addCategoryDialog == null;
    });
  }

  openEditCategoryDialog(value){
    let editCategoryDialog = this._dialog.open(EditCategoryDialogComponent,{
      disableClose:false,
      autoFocus:false,
      data:{value}
    });
    editCategoryDialog.afterClosed().subscribe(
    res => {
      if(res){
        this.GetAllCategories();
      }
      editCategoryDialog == null;
    });
  }
  openDeleteConfirmationDialog(Id:number){
    let deleteCategoryDialog = this._dialog.open(DeleteCategoryConfirmationDialogComponent,{
      disableClose:false,
      autoFocus:false,
    });
    deleteCategoryDialog.afterClosed().subscribe(
    res => {
      if(res){
        this.DeleteCategory(Id);
      }
      deleteCategoryDialog == null;
    });
  }

  onNoClick(event){
    this.openCategoryOperationDialog.close();
    event.preventDefault();
    return false;
  }

}
