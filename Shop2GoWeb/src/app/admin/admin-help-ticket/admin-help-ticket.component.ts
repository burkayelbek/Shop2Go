import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/shared/admin.service';
import { UserService } from 'src/app/shared/user.service';
import {IDeleteTicketModel, IGetAllTickets} from '../../shared/interfaces';
import { AdminHelpTicketDetailsDialogComponent } from '../dialogs/admin-help-ticket-details-dialog/admin-help-ticket-details-dialog.component';

@Component({
  selector: 'app-admin-help-ticket',
  templateUrl: './admin-help-ticket.component.html',
  styleUrls: ['./admin-help-ticket.component.css']
})
export class AdminHelpTicketComponent implements OnInit,AfterViewInit {
  toggleShowStatus = true;
  toggleDeleteButton = false;
  ticketStorage:IGetAllTickets[];
  
  tickets:any[] = [];
  displayedColumns: string[] = ['Select','Order','UserID','UserFullName','Title', 'Priority', 'Status', 'CreatedOn', 'FixedDate','Details&Operation'];
  dataSource = new MatTableDataSource(this.tickets);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  TotalRow: number;  
  selection = new SelectionModel<any>(true, []); 

  constructor( 
    private _adminService:AdminService,
    public _dialog: MatDialog,
    private _toastr:ToastrService
    ){
    }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.GetAllTickets();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  DeleteSelectedTicket() {  
    debugger;
    var numSelected = this.selection.selected;
    let model: IDeleteTicketModel={Id:[]};
    numSelected.forEach(item => 
    { 
      model.Id.push(item.Id)
    });
      console.log(model);
    if (numSelected != null) {  
      this._adminService.DeleteSelectedTicket(model).subscribe(
        (res:any) => {  
          this._toastr.success('Ticket is successfully deleted!','Success');
          this.GetAllTickets();  
      },
      err=>{
        this._toastr.error('You should choose at least 1 ticket!','Error');
      })  
          
    } 
  }

  GetAllTickets(){
    this.tickets=[];
    this._adminService.GetAllTickets().subscribe(
      (res:any) => {
        this.ticketStorage = res;
        res.forEach(element => {
          if(this.toggleShowStatus && element.Status == 0){ // Show the current status of datasource (0/1)
            this.tickets.push(element);
          }
          else if(!this.toggleShowStatus && element.Status == 1){
            this.tickets.push(element);
          }
        });
        this.dataSource.data = this.tickets;
    },
    err=>{
      console.log(err);
    });
  }

  ToggleShowStatus(){
    this.toggleShowStatus =!this.toggleShowStatus;
    if (!this.toggleShowStatus) {
      this.toggleDeleteButton = true;
    }
    else if(this.toggleShowStatus){
      this.toggleDeleteButton = false;
    }
    this.tickets=[];
    this.ticketStorage.forEach(element => {
      if(this.toggleShowStatus && element.Status == 0){ // Show the current status of datasource (0/1)
        this.tickets.push(element);
      }
      else if(!this.toggleShowStatus && element.Status == 1){
        this.tickets.push(element);
      }
    });
    this.dataSource.data = this.tickets;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openTicketDetailsDialog(value){
    let dialogRef = this._dialog.open(AdminHelpTicketDetailsDialogComponent,{
      data:{value}
    });
    dialogRef.afterClosed().subscribe(
    res => {
        this.GetAllTickets();
    });
  }
  DeleteTicketByuserId(id:string){
    this._adminService.DeleteTicketByuserId(id).subscribe(
    res => {
      this._toastr.success('Ticket is successfully deleted!','Success');
      this.GetAllTickets();
    },
    err=>{
      this._toastr.error('Ticket could not deleted!','Error')
      console.log(err);
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