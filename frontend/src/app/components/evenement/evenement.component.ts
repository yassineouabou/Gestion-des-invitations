import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EvenementService } from '../../services/evenement.service';
import { EvenementDto } from '../../models/Evenement.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-evenement',
  templateUrl: './evenement.component.html',
  styleUrl: './evenement.component.css'
})
export class EvenementComponent implements OnInit {

  visible:boolean=false;
  visibleLien:boolean=false;
  evenementForm!:FormGroup;
  savedEvenement!:EvenementDto;
  registrationLink: string = '';
  evenements:EvenementDto[]=[];
  
  searchValue: string = '';
  loading: boolean = false;
  selectedEvent!: EvenementDto | null;
  eventIdToDelete: number | null = null;

  constructor(private fb:FormBuilder,
    private evenementService:EvenementService,
    private messageService:MessageService,
    private confirmationService: ConfirmationService
  ){}

  ngOnInit(): void {
    this.evenementForm = this.fb.group({
    titre: ['', Validators.required],
    categorie: ['', Validators.required],
    dateEvenement: ['', Validators.required],
    lieu: ['', Validators.required]
    });
    
    this.loadEvenements();
  }

  loadEvenements(): void {
    this.loading = true;
    this.evenementService.getAllEvenement().subscribe({
      next: (data) => {
        this.evenements = data;
        this.loading = false;
        console.log(this.evenements);
      },
      error: (err) => {
        console.error('Erreur de chargement des événements :', err);
        this.loading = false;
      }
    });
  }

   editEvent(e: EvenementDto) {
    this.selectedEvent = e;
    this.evenementForm.patchValue(e);
    this.visible = true;
  }

  showDialog(){
    this.visible=true;
  }


   onSubmit() {
    if (this.evenementForm.valid) {
      const evenementData: EvenementDto = this.evenementForm.value;

      if (this.selectedEvent) {
        // update
        this.evenementService.updateEvenement(evenementData, this.selectedEvent.id).subscribe({
          next: () => {
            this.loadEvenements();
            this.visible = false;
            this.messageService.add({ severity: 'success', summary: 'Success', detail: "mise à jour de l'événement réussie" });
            this.selectedEvent = null;
            
          },
          error: err => {
            console.error("Erreur lors de la mise à jour", err);
          }
        });
      } else {
        // create
        this.evenementService.addEvent(evenementData).subscribe({
          next: (data) => {
            this.savedEvenement = data;
            this.registrationLink = this.savedEvenement.lien;
            this.visible = false;
            this.visibleLien = true;
            this.loadEvenements();
            this.messageService.add({ severity: 'success', summary: 'Success', detail: ' événement ajouté avec succès' });
          },
          error: (err) => {
            console.error("Erreur lors de l'ajout", err);
          }
        });
      }
      this.evenementForm.reset();
    }
  }

  confirmDelete(event: Event, id: number) {
  this.eventIdToDelete = id;
  this.confirmationService.confirm({
    target: event.target as EventTarget,
    message: 'Voulez-vous vraiment supprimer cet événement ?',
    header: 'Confirmation',
    icon: 'pi pi-exclamation-triangle',
    acceptButtonStyleClass: 'bg-red-600 hover:bg-red-700 text-white mx-2 px-4 py-2 rounded-md text-sm',
    rejectButtonStyleClass: 'bg-gray-200 hover:bg-gray-300 text-gray-700 px-4 py-2 rounded-md text-sm ml-2',
    
    accept: () => {
      if (this.eventIdToDelete !== null) {
        this.evenementService.deleteEvenement(this.eventIdToDelete).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Supprimé', detail: 'Événement supprimé', life: 3000 });
            this.loadEvenements();
            this.eventIdToDelete = null;
          },
          error: (err) => {
            console.error('Erreur de suppression', err);
            this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Suppression échouée', life: 3000 });
          }
        });
      }
    },
    reject: () => {
      this.messageService.add({ severity: 'info', summary: 'Annulé', detail: 'Suppression annulée', life: 3000 });
    }
  });
}


  clear(table: any): void {
    table.clear();
    this.searchValue = '';
  }

  showLink(link:string){
    this.registrationLink = link;
    this.visibleLien = true;
  }

  copyLink() {
    if (this.registrationLink) {
      navigator.clipboard.writeText(this.registrationLink)
        .then(() => {
          // Optionnel : afficher une notification
          this.visibleLien = false;
        })
        .catch(err => {
          console.error('Erreur lors de la copie : ', err);
        });
    }
  }

  exportCSV() {
    const data = this.evenements.map(evenement=>{
      return {
        titre:evenement.titre,
        catzgorie:evenement.categorie,
        date:evenement.dateEvenement,
        lieu:evenement.lieu
      };
    })
    const worksheet = XLSX.utils.json_to_sheet(data);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Données');

    const csvBuffer = XLSX.write(workbook, { bookType: 'csv', type: 'array' });
    const blob = new Blob([csvBuffer], { type: 'text/csv;charset=utf-8;' });

    saveAs(blob, 'evenements.csv');
  }




}
