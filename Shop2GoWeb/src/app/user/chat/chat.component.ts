import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit,AfterViewChecked {
  @ViewChild('scrollMe') private myScrollContainer: ElementRef;
  conversations:any[] = [];
  chatHistory:any[] = [];
  convId;
  chatDetails;
  userId;
  ProfilePicture:any;
  participantId;
  sendMessageFieldForm:FormGroup;
  showVisible = true;

  conversationStorage:any[];

  constructor
  (
    private _userService:UserService,
    private _toastr:ToastrService,
    private _formbuilder:FormBuilder,
    private _route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.grabUserIdFromToken();
    this.GetMessagesByUser();
    this.scrollToBottom();
    
    this.sendMessageFieldForm = this._formbuilder.group({
      Message:['',[
        Validators.maxLength(200)
      ]]
    });

    this._route.params.subscribe((res: Params) => {
      let id = res['id'];
      this.participantId = id;
      this.GetMessagesByUserId(id);
    });

  }

  ngAfterViewChecked() {        
    this.scrollToBottom();        
  } 

  scrollToBottom(): void {
    try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    } 
    catch(err){ 
    }                 
  }

  isArrayEmpty(arr:any[]){
    return (arr === undefined || arr.length==0);
   }

  grabUserIdFromToken(){
    const token = localStorage.getItem('token') as string | undefined;
    const helper = new JwtHelperService();
    this.userId = helper.decodeToken(token).UserID;
  }

  GetMessagesByUser(){
    this._userService.GetMessagesByUser().subscribe(
      (res:any) => {
        this.conversations = res;
        console.log(res);
      },
      err=>{  
        this._toastr.error('Failed to receive conversations.', 'Error');
        console.error(err);
    });
  }

  openMessages(conversationId : string){
    this.showVisible = !this.showVisible;
    this._userService.GetMessagesByConversationId(conversationId).subscribe(
      (res:any) => {
        this.chatHistory = res.history as [];
        this.chatDetails = res.conversationDetails;
        this.participantId = res.conversationDetails.ParticipantId;

      },
      err => {
        this._toastr.error('Failed to receive conversation history.', 'Error');
        console.error(err);
      }
    );
  }
  SendMessage(){
    if(this.sendMessageFieldForm.invalid || this.participantId === undefined || this.participantId === ""){
      this._toastr.warning('Can\'t send this message');
      return;
    }
    const model = {
      RecieverId: this.participantId,
      Message: this.sendMessageFieldForm?.get('Message')?.value
    }
    if(this.sendMessageFieldForm?.get('Message')?.value.trim() != ""){
      this._userService.SendMessage(model).subscribe(
        (res:any)=>{
          this.chatDetails = res;
          this.openMessages(res.ConversationId);
          this.sendMessageFieldForm?.get('Message')?.reset();
        },
        err=>{
          console.log(err);
        });
    }
   
  }

  GetMessagesByUserId(id:string){
    this._userService.GetMessagesByUserId(id).subscribe(
    (res:any) => {
      this.chatHistory = res.history as [];
      this.chatDetails = res.conversationDetails;
    },
    err=>{
      console.log(err);
    });
  }

}
