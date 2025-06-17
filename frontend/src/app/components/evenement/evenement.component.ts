import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EvenementService } from '../../services/evenement.service';
import { EvenementDto } from '../../models/Evenement.model';

@Component({
  selector: 'app-evenement',
  templateUrl: './evenement.component.html',
  styleUrl: './evenement.component.css'
})
export class EvenementComponent implements OnInit {

  visible:boolean=false;
  visibleLien:boolean=false;
  evenementForm!:FormGroup;
  savedOrganisateur!:EvenementDto;
  registrationLink: string = '';
  evenements:EvenementDto[]=[];

  searchValue: string = '';
  loading: boolean = false;

  constructor(private fb:FormBuilder,
    private evenementService:EvenementService
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

  showDialog(){
    this.visible=true;
  }

  onSubmit(){
    if (this.evenementForm.valid) {
    const evenementData:EvenementDto = this.evenementForm.value;
    this.evenementService.addEvent(evenementData).subscribe({
      next: (data) => {
        this.savedOrganisateur = data;
        this.registrationLink = this.savedOrganisateur.lien;
        this.visible = false;
        this.evenementForm.reset();
        this.visibleLien = true;
        this.loadEvenements();
        console.log(this.savedOrganisateur);

      },
      error: (err) => {
        console.error("Erreur lors de l'ajout de l'événement", err);
      }
    });
  }
  }

  clear(table: any): void {
    table.clear();
    this.searchValue = '';
  }

  showLink(link:string){
    this.registrationLink = link;
    this.visibleLien = true;
  }
}
