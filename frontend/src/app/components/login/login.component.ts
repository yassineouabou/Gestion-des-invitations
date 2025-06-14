import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from '../../models/Login.model';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!:FormGroup;
  isErreur:boolean = false;

  constructor(private fb:FormBuilder,
    private authService:AuthService
  ){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email:this.fb.control(""),
      password:this.fb.control("")
    })
  }

  handleLogin(){
    let login:Login = this.loginForm.value;
    this.authService.login(login).subscribe({
      next:(data)=>{
        this.authService.loadProfile(data);
        this.isErreur = false;
        console.log(data);
      },
      error:(err)=>{
        this.isErreur=true;
        console.log(err.error);
      }
    })
  }
}
