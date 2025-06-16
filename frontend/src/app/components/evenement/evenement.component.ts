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

  constructor(private fb:FormBuilder,
    private evenementService:EvenementService
  ){}

  ngOnInit(): void {
    this.evenementForm = this.fb.group({
    titre: ['', Validators.required],
    categorie: ['', Validators.required],
    dateEvenement: ['', Validators.required],
    lieu: ['', Validators.required]
    })    
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
        console.log(this.savedOrganisateur);

      },
      error: (err) => {
        console.error("Erreur lors de l'ajout de l'événement", err);
      }
    });
  }
  }
}
