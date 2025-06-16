import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthService } from '../../services/auth.service';

import { Router } from '@angular/router';
import { Login } from '../../models/Login.model';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!:FormGroup;
  visible: boolean = false;

  constructor(private fb:FormBuilder,
    private authService:AuthService,
     private messageService:MessageService,
    private router:Router
  ){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email:this.fb.control(""),
      password:this.fb.control("")
    })
  }

  handleLogin(){
    let login:Login= this.loginForm.value;
    this.authService.login(login).subscribe({
      next:(data)=>{
        this.authService.loadProfile(data);
        this.router.navigate(["/home/dashboard"]);
      },
      error:(err)=>{
        
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error });
      }
    })
  }

   showDialog() {
        this.visible = true;
    }

  switchToRegister(){
    this.router.navigate(["/register"]);
  }
}
