import { Injectable } from '@angular/core';
import { environment } from '../environements/environement';
import { HttpClient } from '@angular/common/http';
import { Route } from '@angular/router';
import { Login } from '../models/Login.Model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  api:string=environment.apiUrl+"/organisateur";

  isAuthenticated:boolean=false;
  organisateurId!:number;

  constructor(private http:HttpClient,
  ) { }

  login(login:Login){
    return this.http.post(this.api+"/login",login);
  }

  loadProfile(){
    this.isAuthenticated = true;
    
  }
}
