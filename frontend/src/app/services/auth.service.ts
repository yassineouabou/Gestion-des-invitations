import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { Route } from '@angular/router';
import { OrganisateurDto } from '../../models/Organisateur.model';
import { Login } from '../../models/Login.model';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  api:string=environment.apiUrl+"/organisateur";

  isAuthenticated:boolean=false;
  organisateur!:OrganisateurDto;

  constructor(private http:HttpClient,
  ) { }

  login(login:Login){
    return this.http.post(this.api+"/login",login);
  }

  loadProfile(organisateur:any){
    this.isAuthenticated = true;
    this.organisateur = organisateur;
  }
}
