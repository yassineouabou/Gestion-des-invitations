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

  getEvenementById(id:number):Observable<EvenementDto>{
    return this.http.get<EvenementDto>(this.api+"/"+id);
  }

  getAllEvenement():Observable<EvenementDto[]>{
    const organisateurId = this.authService.getOrganisateurId();
     return this.http.get<EvenementDto[]>(this.api+"/organisateur/"+organisateurId);
  }

  updateEvenement(evenement:EvenementDto,evenementId:number):Observable<void>{
    return this.http.put<void>(this.api+"/"+evenementId,evenement);
  }

  deleteEvenement(evenementId:number):Observable<void>{
    return this.http.delete<void>(this.api+"/"+evenementId);
  }

}
