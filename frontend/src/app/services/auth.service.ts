import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { OrganisateurDto } from '../models/Organisateur.model';
import { Login } from '../models/Login.model';
import { RegisterDto } from '../models/Register.model';
import { Router } from '@angular/router';





@Injectable({
  providedIn: 'root'
})
export class AuthService {

  api:string=environment.apiUrl+"/organisateur";

  isAuthenticated:boolean=false;
  organisateur!:OrganisateurDto;

  constructor(private http:HttpClient,
    private router:Router
  ) 
  {
    const storedOrganisateur = localStorage.getItem('organisateur');
    if (storedOrganisateur) {
      this.organisateur = JSON.parse(storedOrganisateur);
      this.isAuthenticated = true;
    }
   }

  login(login:Login){
    return this.http.post(this.api+"/login",login);
  }

  loadProfile(organisateur:any){
    this.isAuthenticated = true;
    this.organisateur = organisateur;
    localStorage.setItem('organisateur', JSON.stringify(organisateur));
  }

  register(register:RegisterDto){
    return this.http.post(this.api+"/register",register);
  }

  logout() {
  this.isAuthenticated = false;
  this.organisateur = null as any;
  localStorage.removeItem('organisateur');
  this.router.navigate(["/login"]);
  }

   getOrganisateurId(): number | null {
    if (this.isAuthenticated) {
      return this.organisateur?.id || null;
    }
    return null;
  }

}
