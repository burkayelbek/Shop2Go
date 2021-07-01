import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import {IDeleteTicketModel, IGetAllTickets} from '../../shared/interfaces';
import { HelpTicketDetailsDialogComponent } from '../dialogs/help-ticket-details-dialog/help-ticket-details-dialog.component';

@Component({
  selector: 'app-my-tickets',
  templateUrl: './my-tickets.component.html',
  styleUrls: ['./my-tickets.component.css']
})
export class MyTicketsComponent implements OnInit,AfterViewInit {
  toggleShowStatus = true;
  toggleDeleteButton = false;

  ticketStorage:IGetAllTickets[];
  ownTickets:any[] = [];
  
  displayedColumns: string[] = ['Select','Order', 'Id','Title', 'Priority', 'CreatedOn','Status','FixedDate','Operation' ];
  dataSource = new MatTableDataSource(this.ownTickets);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  TotalRow: number;  
  selection = new SelectionModel<any>(true, []); 

  constructor(
    private _userService:UserService,
    private _toastr:ToastrService,
    public _dialog: MatDialog,

  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.GetTicketByUser();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  DeleteSelectedTicket() {  
    var numSelected = this.selection.selected;
    let model: IDeleteTicketModel={Id:[]};
    numSelected.forEach(item => 
      { 
        model.Id.push(item.Id)
      });
      console.log(model);
    if (numSelected != null) {  
      this._userService.DeleteSelectedTicket(model).subscribe(
        res => {  
          this._toastr.success('Ticket is successfully deleted!','Success');
          this.GetTicketByUser();  
      },
      err=>{
        this._toastr.error('You should choose at least 1 ticket!','Error');
      })  
          
    } 
  }

  DeleteTicket(id:string){
    this._userService.DeleteTicket(id).subscribe(
    res => {
      this._toastr.success('Ticket is successfully deleted!','Success');
      this.GetTicketByUser();
    },
    err=>{
      this._toastr.error('Ticket could not deleted!','Error')
      console.log(err);
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  GetTicketByUser(){
    this.ownTickets=[];
    this._userService.GetTicketByUser().subscribe(
    (res:any) => {
      this.ticketStorage = res;
      this.ticketStorage.forEach(element => {
        if(this.toggleShowStatus && element.Status == 0){ // Show the current status of datasource (0/1)
          this.ownTickets.push(element);
        }
        else if(!this.toggleShowStatus && element.Status == 1){
          this.ownTickets.push(element);
        }
      });
      this.dataSource.data = this.ownTickets;
    },
    err=>{
      if(err.status == 404 || err.status == 400){
        this.ticketStorage = [];
      }
    });
  }

  ToggleShowStatus(){
    this.toggleShowStatus =!this.toggleShowStatus;
    if (!this.toggleShowStatus) {
      this.toggleDeleteButton = true;
    }
    else if (this.toggleShowStatus) {
      this.toggleDeleteButton = false;
    }
    this.ownTickets=[];
    this.ticketStorage.forEach(element => {
      if(this.toggleShowStatus && element.Status == 0){ // Show the current status of datasource (0/1)
        this.ownTickets.push(element);
      }
      else if(!this.toggleShowStatus && element.Status == 1){
        this.ownTickets.push(element);
      }
    });
    this.dataSource.data = this.ownTickets;
  }

  openTicketDetailsDialog(value){
    let dialogRef = this._dialog.open(HelpTicketDetailsDialogComponent,{
      width:'500px',
      data:{value},
      autoFocus:false
    });
    dialogRef.afterClosed().subscribe(
    res => {
      if(dialogRef.afterClosed()){
        this.GetTicketByUser();
      }
    });
  }

  /* Whether the number of selected elements matches the total number of rows. */  
  isAllSelected() {  
    const numSelected = this.selection.selected.length;  
    const numRows = !!this.dataSource && this.dataSource.data.length;  
    return numSelected === numRows;  
  }  
  /* Selects all rows if they are not all selected; otherwise clear selection. */  
  masterToggle() {  
    this.isAllSelected() ? this.selection.clear() : this.dataSource.data.forEach(r => this.selection.select(r));  
  }  
  /* The label for the checkbox on the passed row */  
  checkboxLabel(row): string {  
    if (!row) {  
        return `${this.isAllSelected() ? 'select' : 'deselect'} all`;  
    }  
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.UserId + 1}`;  
  }  


}
