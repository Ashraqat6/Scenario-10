import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenService } from '../Service/authen.service';
import { LoginDTO } from '../_models/LoginDTO';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm: FormGroup;

constructor(private formBuilder:FormBuilder,
  private authService: AuthenService, private route: Router) {
  this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
    }
    isLoggedIn=false
    email:string=""
    password:string="" 
    Id:string=""; 
    ngOnInit():void{
      this.loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]]
      });
    }    
    onSubmit(e:Event) {
      e.preventDefault();  
      var credentials = new LoginDTO();
      credentials.email = this.loginForm.get('email')?.value ?? '';
      credentials.password = this.loginForm.get('password')?.value ?? '';
      
      this.authService.login(credentials).subscribe((token) => {
        
        this.Id=token.id;
        this.authService.isAuth$.subscribe(
          value => {
            this.isLoggedIn = value;
            if (value) {
              this.route.navigateByUrl('/home');
            } else {
            }
          }
        );

      });
    } 
}
