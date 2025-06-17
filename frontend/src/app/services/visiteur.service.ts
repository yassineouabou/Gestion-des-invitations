import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { VisiteurDto } from '../models/Visiteur.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VisiteurService {

  api:string=environment.apiUrl+"/visiteur";
  constructor(private http:HttpClient) { }

  addVisiteur(visiteur:VisiteurDto,evenementId:number):Observable<void>{
    return this.http.post<void>(this.api+"/"+evenementId,visiteur);
  }
}
