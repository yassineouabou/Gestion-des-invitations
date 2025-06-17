import { Component, OnInit } from '@angular/core';
import { VisiteurService } from '../../services/visiteur.service';
import { VisiteurDto } from '../../models/Visiteur.model';
import { ConfirmationService, MessageService } from 'primeng/api';


@Component({
  selector: 'app-visiteur',
  templateUrl: './visiteur.component.html',
  styleUrl: './visiteur.component.css'
})
export class VisiteurComponent implements OnInit {
  
  visiteurs!:VisiteurDto[];

  loading = false;
  searchValue = '';

  constructor(private visiteurService:VisiteurService,
    private messageService:MessageService,
    private confirmationService:ConfirmationService
  ){}

  ngOnInit(): void {
    this.loadVisiteurs();
  }

  loadVisiteurs(): void {
    this.loading = true;
    this.visiteurService.getAllVisiteur().subscribe({
      next: (data) => {
        this.visiteurs = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur lors du chargement des visiteurs:', err);
        this.messageService.add({
          severity: 'error',
          summary: 'Erreur',
          detail: 'Impossible de charger les visiteurs'
        });
        this.loading = false;
      }
    });
  }

  clear(table: any): void {
    table.clear();
    this.searchValue = '';
  }

  

  deleteVisiteur(visiteur: VisiteurDto): void {
    this.confirmationService.confirm({
      message: `Êtes-vous sûr de vouloir supprimer le visiteur "${visiteur.id}" ?`,
      header: 'Confirmer la suppression',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => {
        this.performDelete(visiteur);
      }
    });
  }

  private performDelete(visiteur: VisiteurDto): void {
    this.visiteurService.deleteVisiteur(visiteur.id).subscribe({
      next: () => {
        this.loadVisiteurs();
        this.messageService.add({
          severity: 'success',
          summary: 'Succès',
          detail: 'Visiteur supprimé avec succès'
        });
      },
      error: (err) => {
        console.error('Erreur lors de la suppression:', err);
        this.messageService.add({
          severity: 'error',
          summary: 'Erreur',
          detail: 'Impossible de supprimer le visiteur'
        });
      }
    });
  }


}
