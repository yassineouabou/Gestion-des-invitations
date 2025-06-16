import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { EvenementDto } from '../models/Evenement.model';
import { Observable } from 'rxjs';
import { OrganisateurDto } from '../models/Organisateur.model';

@Injectable({
  providedIn: 'root'
})
export class EvenementService {

  api:string=environment.apiUrl+"/evenement";

  constructor(private http:HttpClient) { }

  addEvent(evenement:EvenementDto,organisateurId:number):Observable<EvenementDto>{
    return this.http.post<EvenementDto>(this.api+"/"+organisateurId,evenement);
  }

}
