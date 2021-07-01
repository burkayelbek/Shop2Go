import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { faShare } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-help-ticket',
  templateUrl: './help-ticket.component.html',
  styleUrls: ['./help-ticket.component.css']
})
export class HelpTicketComponent implements OnInit{
  ticketForm;
  selected;
  sendIcon = faShare;
  constructor
  (
    private _formBuilder:FormBuilder,
    private _userService:UserService,
    private _toastrService:ToastrService
  ) { }

  ngOnInit(){
    this.ticketForm = this._formBuilder.group({
      Title:['',[
        Validators.required,
        Validators.maxLength(12)
      ]],
      Message:['',[
        Validators.required,
        Validators.maxLength(300)
      ]],
      Priority:['',[ 
        Validators.required
      ]],
    })
  }

  get Message(){
    return this.ticketForm.get('Message');
  }

  get Priority(){
    return this.ticketForm.get('Priority');
  }

  SendUserTicket(data){
    if(this.Priority === "" && this.Message === "" && !this.ticketForm.valid){
      this._toastrService.error('Error!','Error');
      return;
    }
    this._userService.SendUserTicket(data).subscribe(
    (res:any) => {
      this._toastrService.success(res.message,'Success');
      this.ticketForm.reset();
    },
    err=>{
      this._toastrService.error('You have to fill required areas!','Error');
      console.log(err);
    });

  }

}
