import { Component, OnInit } from '@angular/core';
import { VisiteurService } from '../../services/visiteur.service';
import { VisiteurDto } from '../../models/Visiteur.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

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

  

  confirmDelete(event:Event,visiteur:VisiteurDto) {
  this.confirmationService.confirm({
    target: event.target as EventTarget,
    message: 'Voulez-vous vraiment supprimer cet visiter ?',
    header: 'Confirmation',
    icon: 'pi pi-exclamation-triangle',
    acceptButtonStyleClass: 'bg-red-600 hover:bg-red-700 text-white mx-2 px-4 py-2 rounded-md text-sm',
    rejectButtonStyleClass: 'bg-gray-200 hover:bg-gray-300 text-gray-700 px-4 py-2 rounded-md text-sm ml-2',
    
    accept: () => {
      this.performDelete(visiteur);
    },
    reject: () => {
      this.messageService.add({ severity: 'info', summary: 'Annulé', detail: 'Suppression annulée', life: 3000 });
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

  exportCSV() {
      const exportData = this.visiteurs.map(visiteur => {
        return {
          nom: visiteur.nom,
          email: visiteur.email,
          phone: visiteur.phone,
          evenements: visiteur.evenements.length > 0
            ? visiteur.evenements.map(e => e.titre).join(', ')
            : 'Aucun'
        };
      });
      const worksheet = XLSX.utils.json_to_sheet(exportData);
      const workbook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(workbook, worksheet, 'Données');
  
      const csvBuffer = XLSX.write(workbook, { bookType: 'csv', type: 'array' });
      const blob = new Blob([csvBuffer], { type: 'text/csv;charset=utf-8;' });
  
      saveAs(blob, 'visiteurs.csv');
    }
  

}
