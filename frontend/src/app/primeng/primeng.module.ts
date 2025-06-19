import { NgModule } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { TagModule } from 'primeng/tag';
import { SliderModule } from 'primeng/slider';
import { CalendarModule } from 'primeng/calendar';
import { TabViewModule } from 'primeng/tabview';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

const PRIMENG = [
  DialogModule,
  ButtonModule,
  InputTextModule,
  ToastModule,
  ConfirmDialogModule,
  TagModule, 
  DropdownModule, 
  MultiSelectModule, 
  ProgressBarModule,
  TableModule,
  DropdownModule,
  MultiSelectModule,
  ProgressBarModule,
  TagModule,
  SliderModule,
  ButtonModule,
  InputTextModule,
  CalendarModule,
  ConfirmPopupModule,
  TabViewModule,
  ProgressSpinnerModule
  
  
]
@NgModule({
  declarations: [],
  imports: [
    ...PRIMENG,

  ],
  exports:[
    ...PRIMENG,
  ],
  providers:[ConfirmationService,MessageService]
})
export class PrimengModule { }