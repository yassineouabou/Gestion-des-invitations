import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { OrganisateurDto } from '../../models/Organisateur.model';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  
  organisateur!:OrganisateurDto;

  constructor(private authService:AuthService){}
  ngOnInit(): void {
    this.organisateur = this.authService.organisateur;
  }
  
}
