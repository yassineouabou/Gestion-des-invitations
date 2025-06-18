import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FileText, LucideAngularModule,Home, Calendar, Users, House, Mail, UserRound,LayoutGrid,ChartPie,CalendarRange,LogOut  } from 'lucide-angular';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EvenementComponent } from './components/evenement/evenement.component';
import { HomeComponent } from './components/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PrimengModule } from './primeng/primeng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { InscriptionComponent } from './components/inscription/inscription.component';
import { VisiteurComponent } from './components/visiteur/visiteur.component';
import { InvitationComponent } from './components/invitation/invitation.component';



@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    DashboardComponent,
    EvenementComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    InscriptionComponent,
    VisiteurComponent,
    InvitationComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FormsModule,
    LucideAngularModule.pick({ Home, Calendar, Mail, Users, FileText,House, UserRound,LayoutGrid,ChartPie,CalendarRange,LogOut  }),
    PrimengModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
