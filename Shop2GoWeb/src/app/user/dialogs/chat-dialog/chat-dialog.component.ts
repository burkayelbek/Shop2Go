import { AfterViewChecked, Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HomeComponent } from 'src/app/home/home.component';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-chat-dialog',
  templateUrl: './chat-dialog.component.html',
  styleUrls: ['./chat-dialog.component.css']
})
export class ChatDialogComponent implements OnInit,AfterViewChecked {
  @ViewChild('scrollMe') private myScrollContainer: ElementRef;
  sendMessageFieldForm:any;
  chatHistory:any[] = [];
  chatDetails;
  userId
  constructor
  (
    private _userService:UserService,
    private _formbuilder:FormBuilder,
    
    @Inject(MAT_DIALOG_DATA) private data:{RecieverId:string},
    public dialogRef: MatDialogRef<HomeComponent>
  ) { 

  }

  ngOnInit(): void {
    this.grabUserIdFromToken();
    this.sendMessageFieldForm = this._formbuilder.group({
      RecieverId:[this.data.RecieverId,[
      ]],
      Message:['',[
      ]]
    });
    this.scrollToBottom();

    this.GetMessagesByUserId(this.data.RecieverId);
  }

  ngAfterViewChecked() {        
    this.scrollToBottom();        
  } 

  grabUserIdFromToken(){
    const token = localStorage.getItem('token') as string | undefined;
    const helper = new JwtHelperService();
    this.userId = helper.decodeToken(token).UserID;
  }
 
  SendMessage(model){
    this._userService.SendMessage(model).subscribe(
      res=>{
        this.GetMessagesByUserId(this.data.RecieverId);
        this.sendMessageFieldForm.get('Message').reset();
      },
      err=>{
        console.log(err);
      });
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

  scrollToBottom(): void {
    try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    } 
    catch(err){ 
    }                 
  }

}
