import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { UserListDialogComponent } from '../dialogs/user-list-dialog/user-list-dialog.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit,AfterViewInit {
  users:any[]=[];

  displayedColumns: string[] = ['Order','Id','UserName','FullName','Email', 'Status', 'FixedDate','Details&Operation'];
  dataSource = new MatTableDataSource(this.users);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor
  (
   private _userService:UserService,
   public _dialog: MatDialog,
    private _toastr:ToastrService,
  ) { }

  ngOnInit(): void {
  }
  ngAfterViewInit() {
    this.GetAllUsers();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  GetAllUsers(){
    this._userService.GetAllUsers().subscribe(
    (res:any) => {
      this.users = res;
      this.dataSource.data = this.users;
    },
    err=> {
      console.error(err);
    });
  }

  openUserListDialog(value){
    let dialogRef = this._dialog.open(UserListDialogComponent,{
      autoFocus:false,
      data:{value}
    });
    dialogRef.afterClosed().subscribe(
    res => {
        this.GetAllUsers();
    });
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
