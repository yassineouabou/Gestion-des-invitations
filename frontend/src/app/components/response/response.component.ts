import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InvitationService } from '../../services/invitation.service';
import { InvitationDto } from '../../models/Invitation.model';

@Component({
  selector: 'app-response',
  templateUrl: './response.component.html',
  styleUrl: './response.component.css'
})
export class ResponseComponent implements OnInit {

  status:string="";
  invitationId!:number;
  invitation!:InvitationDto;

  constructor(private route:ActivatedRoute,
    private invitationService:InvitationService
  ){}
  
  ngOnInit(): void {
    this.route.queryParams.subscribe(params=>{
      this.invitationId = params['invitationId'];
      this.status =params['status']; 
      if(this.status==="acceptee"){
        this.invitationService.accepter(this.invitationId).subscribe({
          next:(data)=>{
            this.invitation = data;
            console.log(this.invitation);
          },
          error:(err)=>{
            console.log(err.error);
          }
        })
      }else if(this.status==="refusee"){
        this.invitationService.rejeter(this.invitationId).subscribe({
          next:(data)=>{
            this.invitation = data;
          },
          error:(err)=>{
            console.log(err.error);
          }
        })
      }
    })
  }
}
