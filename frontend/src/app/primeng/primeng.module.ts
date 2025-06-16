import { NgModule } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ReactiveFormsModule } from '@angular/forms';

const PRIMENG = [
  DialogModule,
  ButtonModule,
  InputTextModule,
  ToastModule,
  ConfirmDialogModule,
  
  
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