import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { VisiteurDto } from '../models/Visiteur.model';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class VisiteurService {

  api:string=environment.apiUrl+"/visiteur/";
  constructor(private http:HttpClient,
    private authService:AuthService
  ) { }

  addVisiteur(visiteur:VisiteurDto,evenementId:number):Observable<void>{
    return this.http.post<void>(this.api+evenementId,visiteur);
  }

  getAllVisiteur():Observable<VisiteurDto[]>{
    const organisateurId = this.authService.getOrganisateurId();
    return this.http.get<VisiteurDto[]>(this.api+"organisateur/"+organisateurId);
  }

  deleteVisiteur(visiteurId:number):Observable<void>{
    return this.http.delete<void>(this.api+visiteurId);
  }
}
