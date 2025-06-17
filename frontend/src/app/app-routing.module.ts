import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EvenementComponent } from './components/evenement/evenement.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { InscriptionComponent } from './components/inscription/inscription.component';
import { VisiteurComponent } from './components/visiteur/visiteur.component';

const routes: Routes = [
  {path:"login",component:LoginComponent},
  {path:"register",component:RegisterComponent},
  {path:"inscription",component:InscriptionComponent},
  {path:"home",component:HomeComponent,children:[
    {path:"dashboard",component:DashboardComponent},
    {path:"evenement",component:EvenementComponent},
    {path:"visiteur",component:VisiteurComponent},

  ]},
  {path:"",redirectTo:"/login",pathMatch:"full"}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
