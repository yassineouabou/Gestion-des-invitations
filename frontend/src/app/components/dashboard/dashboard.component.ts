import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { OrganisateurDto } from '../../models/Organisateur.model';
import { InvitationService } from '../../services/invitation.service';
import { InvitationDto } from '../../models/Invitation.model';
import { EvenementDto } from '../../models/Evenement.model';
import { EvenementService } from '../../services/evenement.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  
  organisateur!: OrganisateurDto;
  basicData: any;
  invitations!: InvitationDto[];
  evenements!: EvenementDto[];
  basicOptions: any;
  nbrVisiteur:number=0;

  maxParticipants:number=0;
  
  constructor(
    private authService: AuthService,
    private invitationService: InvitationService,
    private evenementService: EvenementService 
  ){}
  
  ngOnInit(): void {
    this.organisateur = this.authService.organisateur;
    this.loadInvitations();
    this.loadEvenementsData();
  }


  private loadInvitations(){
    this.invitationService.getByOrganisateurId().subscribe({
      next:(data)=>{
        this.invitations = data;
        this.nbrVisiteur = this.invitations.filter(i=>i.etat==="ACCEPTEE").length;
      },
      error:(err)=>{
        console.log(err.error);
      }
    })
  }


  private loadEvenementsData(): void {
    // Récupérer les événements de l'organisateur connecté
    this.evenementService.getAllEvenement()
      .subscribe({
        next: (data) => {
          this.evenements = data;
          this.maxParticipants = Math.ceil(Math.max(...this.evenements.map(e => +e.participantes)) / 10) * 10;
          this.setupChartOptions();
          this.setupChartData();
        },
        error: (error) => {
          console.error('Erreur lors du chargement des événements:', error);
          // Fallback avec des données par défaut si nécessaire
          this.setupDefaultChartData();
        }
      });
  }

  private setupChartData(): void {
    if (this.evenements && this.evenements.length > 0) {
      // Créer les labels (titres des événements)
      const labels = this.evenements.map(event => 
        event.titre.length > 15 ? event.titre.substring(0, 15) + '...' : event.titre
      );
      
      // Créer les données (nombre de participants)
      const participantsData = this.evenements.map(event => event.participantes);
      
      // Générer des couleurs dynamiques
      const colors = this.generateColors(this.evenements.length);

      
      
      this.basicData = {
        labels: labels,
        datasets: [
          {
            label: 'Nombre de Participants',
            data: participantsData,
            backgroundColor: colors.backgroundColor,
            borderColor: colors.borderColor,
            borderWidth: 2,
            hoverBackgroundColor: colors.hoverColor,
            hoverBorderWidth: 3
          }
        ]
      };
    } else {
      this.setupDefaultChartData();
    }
  }

  private setupDefaultChartData(): void {
    this.basicData = {
      labels: ['Aucun événement'],
      datasets: [
        {
          label: 'Participants',
          data: [0],
          backgroundColor: ['rgba(201, 203, 207, 0.2)'],
          borderColor: ['rgb(201, 203, 207)'],
          borderWidth: 1
        }
      ]
    };
  }

  private generateColors(count: number): any {
    const baseColors = [
      { bg: 'rgba(255, 99, 132, 0.2)', border: 'rgb(255, 99, 132)', hover: 'rgba(255, 99, 132, 0.4)' },
      { bg: 'rgba(54, 162, 235, 0.2)', border: 'rgb(54, 162, 235)', hover: 'rgba(54, 162, 235, 0.4)' },
      { bg: 'rgba(255, 205, 86, 0.2)', border: 'rgb(255, 205, 86)', hover: 'rgba(255, 205, 86, 0.4)' },
      { bg: 'rgba(75, 192, 192, 0.2)', border: 'rgb(75, 192, 192)', hover: 'rgba(75, 192, 192, 0.4)' },
      { bg: 'rgba(153, 102, 255, 0.2)', border: 'rgb(153, 102, 255)', hover: 'rgba(153, 102, 255, 0.4)' },
      { bg: 'rgba(255, 159, 64, 0.2)', border: 'rgb(255, 159, 64)', hover: 'rgba(255, 159, 64, 0.4)' }
    ];

    const backgroundColor = [];
    const borderColor = [];
    const hoverColor = [];

    for (let i = 0; i < count; i++) {
      const colorIndex = i % baseColors.length;
      backgroundColor.push(baseColors[colorIndex].bg);
      borderColor.push(baseColors[colorIndex].border);
      hoverColor.push(baseColors[colorIndex].hover);
    }

    return { backgroundColor, borderColor, hoverColor };
  }

  private setupChartOptions(): void {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
   

    this.basicOptions = {
      responsive: true,
      maintainAspectRatio: false, // Permet de contrôler la hauteur via CSS
      aspectRatio: 0.7, // Ratio plus petit = graphique plus haut
      plugins: {
        
        legend: {
          display: true,
          labels: {
            color: textColor,
            usePointStyle: true,
            padding: 30
          }
        },
        tooltip: {
          callbacks: {
            afterLabel: function(context: any) {
              return `Événement: ${context.label}`;
            }
          }
        }
      },
      layout: {
        padding: {
          top: 30,
          bottom: 30,
          left: 30,
          right: 30
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          max: this.maxParticipants > 0 ? this.maxParticipants : 200, // Définir une valeur max
          title: {
            display: true,
            text: 'Nombre de Participants',
            color: textColor,
            font: {
              size: 14,
              weight: 'bold'
            }
          },
          ticks: {
            color: textColorSecondary,
            stepSize: 40, // Étapes plus petites pour plus de graduations
            padding: 15,
            font: {
              size: 12
            }
          },
          grid: {
            color: surfaceBorder,
            drawBorder: true,
            lineWidth: 1
          }
        },
        x: {
          title: {
            display: true,
            text: 'Événements',
            color: textColor,
            font: {
              size: 14,
              weight: 'bold'
            }
          },
          ticks: {
            color: textColorSecondary,
            maxRotation: 45,
            minRotation: 0,
            padding: 15,
            font: {
              size: 11
            }
          },
          grid: {
            color: surfaceBorder,
            drawBorder: true,
            lineWidth: 1
          }
        }
      },
      animation: {
        duration: 1000,
        easing: 'easeOutQuart'
      }
    };
  }

  // Méthode utilitaire pour rafraîchir les données
  refreshChartData(): void {
    this.loadEvenementsData();
  }
}