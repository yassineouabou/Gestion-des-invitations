import { NgModule } from '@angular/core';
import { Message } from 'primeng/message';

const PRIMENG:any = [
  Message
  
]

@NgModule({
   declarations: [],
  imports: [
    ...PRIMENG
  ],
  exports:[
    ...PRIMENG,
  ],
})
export class PrimengModule { }
