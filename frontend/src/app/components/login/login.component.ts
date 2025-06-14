import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from '../../models/Login.Model';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!:FormGroup;

  constructor(private fb:FormBuilder){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email:this.fb.control(""),
      password:this.fb.control("")
    })
  }

  handleLogin(){
    let login:Login = this.loginForm.value;
    
  }
}
