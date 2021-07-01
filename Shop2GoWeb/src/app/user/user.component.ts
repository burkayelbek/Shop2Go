import { Component, OnInit } from "@angular/core";
import { UserService } from "../shared/user.service";
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { faTicketAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: "app-user",
  templateUrl: "./user.component.html",
  styleUrls: ["./user.component.css"],
})
export class UserComponent implements OnInit {
  sendTicket = false;
  personalInformation = true;
  myticket= false;
  sendTicketIcon = faPaperPlane;
  myTicketIcon = faTicketAlt;

  constructor(
    private _userService:UserService
  ) {}

  ngOnInit(){

  }

  showTicketPage(){
    this.sendTicket = true;
    this.personalInformation = false;
    this.myticket = false;
  }

  showPersonalInformationPage(){
    this.personalInformation = true;
    this.sendTicket = false;
    this.myticket = false;
  }
  MyTicketPage(){
    this.myticket = true;
    this.sendTicket = false;
    this.personalInformation = false;
  }

  step = 0;
  setStep(index: number) {
    this.step = index;
  }

  roleMatchUser(){
    return this._userService.roleMatch(['User']);
  }

}