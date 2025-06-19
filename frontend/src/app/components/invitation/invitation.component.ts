import { Component, OnInit } from '@angular/core';
import { InvitationDto } from '../../models/Invitation.model';
import { InvitationService } from '../../services/invitation.service';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrl: './invitation.component.css'
})
export class InvitationComponent implements OnInit{

  invitations:InvitationDto[]=[];
  invitationReject:InvitationDto[] =[];
  invitationAcepter:InvitationDto[]=[];
  invitationEnAttent:InvitationDto[]=[];
  invitationEnvoyer:InvitationDto[]=[];


  constructor(private invitationService:InvitationService){}

  ngOnInit(): void {
    this.invitationService.getAllInvitation().subscribe({
      next:(data)=>{
        this.invitations = data;
        this.invitationEnAttent = this.invitations.filter(invitation=>invitation.etat==="EN_ATTENTE");
        this.invitationAcepter = this.invitations.filter(invitation=>invitation.etat==="ACCEPTEE");
        this.invitationReject = this.invitations.filter(invitation=>invitation.etat==="EN_ATTENTE");
        this.invitationEnvoyer = this.invitations.filter(invitation=>invitation.etat==="ENVOYE");

        console.log(this.invitationEnAttent)
      },
      error:(err)=>{
        console.log(err.error);
      }
    })
  }
}
