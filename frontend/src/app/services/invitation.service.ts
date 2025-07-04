import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InvitationDto } from '../models/Invitation.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {

   api:string=environment.apiUrl+"/verification/";

  constructor(private http:HttpClient,
    private authService:AuthService
  ) { }

  getAllInvitation():Observable<InvitationDto[]>{
    const organisateurId = this.authService.getOrganisateurId();
    return this.http.get<InvitationDto[]>(this.api+"organisateur/"+organisateurId);
  }

  sendEmail(id:number):Observable<void>{
    return this.http.get<void>(this.api+"envoyer-email/"+id);
  }

  accepter(id:number):Observable<InvitationDto>{
    return this.http.get<InvitationDto>(this.api+"accepter/"+id);
  }

  rejeter(id:number):Observable<InvitationDto>{
    return this.http.get<InvitationDto>(this.api+"refuser/"+id);
  }


  getInvitation(id:number):Observable<InvitationDto>{
    return this.http.get<InvitationDto>(this.api+"/"+id);
  }

  getByOrganisateurId():Observable<InvitationDto[]>{
    const organisateurId = this.authService.getOrganisateurId();
    return this.http.get<InvitationDto[]>(this.api+"organisateur/"+organisateurId);
  }

}
