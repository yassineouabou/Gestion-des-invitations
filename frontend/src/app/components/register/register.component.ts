import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RegisterDto } from '../../../models/Register.model';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{

  registerForm!:FormGroup;
  isIncorrect:boolean=false;
  error:string="";

  constructor(private fb:FormBuilder,
    private router:Router,
    private authService:AuthService,
    private messageService:MessageService
  ){}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
    nom: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required],
    });
  }

  handleRegister(){
    
      const { password, confirmPassword, nom, email } = this.registerForm.value;

      if (password !== confirmPassword) {
        this.error="Password Incorrect !";
        this.isIncorrect = true;

        return;
      }

      const registerData: RegisterDto = {
        nom,
        email,
        password
      };

      this.authService.register(registerData).subscribe({
        next: () => {
          this.isIncorrect = true;
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Inscription réussie !' });
          
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (err) => {
          console.error(err);
          this.error = "Erreur lors de l'inscription.";
          this.isIncorrect = true;
        }
  });
  }

  switchToLogin(){
    this.router.navigate(["/login"]);
  }

  
}
