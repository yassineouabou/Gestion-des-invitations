import { Component, OnInit } from '@angular/core';
import { InvitationDto } from '../../models/Invitation.model';
import { InvitationService } from '../../services/invitation.service';
import { ConfirmationService, MessageService } from 'primeng/api';

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

  invitationId:number | null = null;
  isSended:boolean=false;

  constructor(private invitationService:InvitationService,
    private confirmationService:ConfirmationService,
    private messageService:MessageService
  ){}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(){
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

  confirmEmailSend(event: Event, id: number) {
  this.invitationId= id;
  this.confirmationService.confirm({
    target: event.target as EventTarget,
    message: 'Voulez-vous vraiment envoyé ce mail ?',
    header: 'Confirmation',
    icon: 'pi pi-exclamation-triangle',
    acceptButtonStyleClass: 'bg-red-600 hover:bg-red-700 text-white mx-2 px-4 py-2 rounded-md text-sm',
    rejectButtonStyleClass: 'bg-gray-200 hover:bg-gray-300 text-gray-700 px-4 py-2 rounded-md text-sm ml-2',
    
    accept: () => {
        if (this.invitationId !== null) {
          this.isSended=true;
          this.invitationService.sendEmail(this.invitationId).subscribe({
            next: () => {
              this.loadData();
              this.isSended=false;
              this.messageService.add({ severity: 'success', summary: 'Envoyé', detail: 'e-mail envoyé avec succès', life: 3000 });
              this.invitationId = null;
            },
            error: (err) => {
              this.isSended=false;
              this.messageService.add({ severity: 'error', summary: 'Erreur', detail: "erreur lors de l'envoie de mail", life: 3000 });
            }
          });
        }
      },
      reject: () => {
        this.messageService.add({ severity: 'info', summary: 'Annulé', detail: 'Envoi annulé', life: 3000 });
      }
    });
  }


  accepter(event: Event, id: number) {
  this.invitationId= id;
  this.confirmationService.confirm({
    target: event.target as EventTarget,
    message: 'Voulez-vous vraiment accpeter cette invitation ?',
    header: 'Confirmation',
    icon: 'pi pi-exclamation-triangle',
    acceptButtonStyleClass: 'bg-red-600 hover:bg-red-700 text-white mx-2 px-4 py-2 rounded-md text-sm',
    rejectButtonStyleClass: 'bg-gray-200 hover:bg-gray-300 text-gray-700 px-4 py-2 rounded-md text-sm ml-2',
    
    accept: () => {
        if (this.invitationId !== null) {
          
          this.invitationService.accepter(this.invitationId).subscribe({
            next: () => {
              this.loadData();
              this.isSended=false;
              this.messageService.add({ severity: 'success', summary: 'Envoyé', detail: 'invitation accepter avec succès', life: 3000 });   
            },
            error: (err) => {
              this.messageService.add({ severity: 'error', summary: 'Erreur', detail: "erreur", life: 3000 });
            }
          });
        }
      },
      reject: () => {
        this.messageService.add({ severity: 'info', summary: 'Annulé', detail: 'Acceptation annulé', life: 3000 });
      }
    });
  }


  rejeter(event: Event, id: number) {
  this.invitationId= id;
  this.confirmationService.confirm({
    target: event.target as EventTarget,
    message: 'Voulez-vous vraiment accpeter cette invitation ?',
    header: 'Confirmation',
    icon: 'pi pi-exclamation-triangle',
    acceptButtonStyleClass: 'bg-red-600 hover:bg-red-700 text-white mx-2 px-4 py-2 rounded-md text-sm',
    rejectButtonStyleClass: 'bg-gray-200 hover:bg-gray-300 text-gray-700 px-4 py-2 rounded-md text-sm ml-2',
    
    accept: () => {
        if (this.invitationId !== null) {
          
          this.invitationService.accepter(this.invitationId).subscribe({
            next: () => {
              this.loadData();
              this.isSended=false;
              this.messageService.add({ severity: 'success', summary: 'Envoyé', detail: 'invitation rejeter avec succès', life: 3000 });   
            },
            error: (err) => {
              this.messageService.add({ severity: 'error', summary: 'Erreur', detail: "erreur", life: 3000 });
            }
          });
        }
      },
      reject: () => {
        this.messageService.add({ severity: 'info', summary: 'Annulé', detail: 'Rejection annulé', life: 3000 });
      }
    });
  }



}
