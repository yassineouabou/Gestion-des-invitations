import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{

  registerForm!:FormGroup;
  constructor(private fb:FormBuilder,
    private router:Router
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
    
  }

  switchToLogin(){
    this.router.navigate(["/login"]);
  }
}
