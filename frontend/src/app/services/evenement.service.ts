import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { EvenementDto } from '../models/Evenement.model';
import { Observable } from 'rxjs';
import { OrganisateurDto } from '../models/Organisateur.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class EvenementService {

  api:string=environment.apiUrl+"/evenement";


  constructor(private http:HttpClient,
    private authService:AuthService
  ) { }

  addEvent(evenement:EvenementDto):Observable<EvenementDto>{
    const organisateurId = this.authService.getOrganisateurId();
    return this.http.post<EvenementDto>(this.api+"/"+organisateurId,evenement);
  }

  getAllEvenement():Observable<EvenementDto[]>{
    const organisateurId = this.authService.getOrganisateurId();
     return this.http.get<EvenementDto[]>(this.api+"/organisateur/"+organisateurId);
  }

}
