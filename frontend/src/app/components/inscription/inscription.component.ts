import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EvenementService } from '../../services/evenement.service';
import { EvenementDto } from '../../models/Evenement.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VisiteurDto } from '../../models/Visiteur.model';
import { VisiteurService } from '../../services/visiteur.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-inscription',
  templateUrl: './inscription.component.html',
  styleUrl: './inscription.component.css'
})
export class InscriptionComponent implements OnInit{

  evenementId!: number;
  evenement!:EvenementDto;
  inscriptionForm!: FormGroup;
  isSended:boolean=true;

  constructor(private route: ActivatedRoute,
    private evenementService:EvenementService,
    private fb: FormBuilder,
    private visiteurService:VisiteurService,
    private messageService:MessageService
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.evenementId = +params['evenementId'];
      this.evenementService.getEvenementById(this.evenementId).subscribe({
        next:(data)=>{
          this.evenement = data;
          this.isSended=false;
        },
        error:(err)=>{
          console.log(err.error);
          this.isSended=false;
        }
      })
      
    });

    this.inscriptionForm = this.fb.group({
      nom: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required]
    });
  }

  onSubmit(){
    if(this.inscriptionForm.valid){
      const inscriptionData:VisiteurDto = this.inscriptionForm.value;
      this.visiteurService.addVisiteur(inscriptionData,this.evenementId).subscribe({
        next:(data)=>{
          console.log("added");
          
          this.messageService.add({ severity: 'success', summary: 'Success', detail: "invitation envoyée avec succès" });
        },
        error:(err)=>{
          this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error });
        }
      });
      this.inscriptionForm.reset();
    }
  }

}
