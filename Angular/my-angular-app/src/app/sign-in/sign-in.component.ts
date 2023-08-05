import { HttpClient } from '@angular/common/http';
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterDTO } from '../_models/RegisterDTO';
import { RegisterService } from '../Service/register.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnChanges {
  
  user = new RegisterDTO('', '', '', '', '', '');
  formIsValid: boolean = false;
  showSection: boolean = false;
 
  constructor(private registerService: RegisterService,public http:HttpClient , private route:Router) {}
  ngOnChanges(changes: SimpleChanges): void {
    this.registerEnable();
  }

  //#region registration
  registerUser() {
    console.log(this.user);
    
      this.registerService.registerUser(this.user).subscribe(response => {
        
        this.returnToLogin();
      },
      error => {
        alert('Registration failed Your Email Or Mobile is not Unique' );
      
      });
  }

      //#region redirect to loginpage
    returnToLogin()
    {
      this.route.navigate(['/login']);
    }
      //#endregion



      //#region  validation 
      isValidEmail(email: string): boolean {
        // check if email is valid using regular expression
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
      }
      
      isValidPassword(password: string): boolean {
        // check if password is at least 6 characters long, has one uppercase letter, one lowercase letter, and one special character
        const passwordRegexPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;
        return passwordRegexPattern.test(password);
      }
      
      isValidConfirmPassword(password: string, confirmPassword: string): boolean {
        // check if password and confirm password match
        return password === confirmPassword;
      }
      
      isValidPhoneNumber(phoneNumber: string): boolean {
        // check if phone number is numeric and has 11 digits
        const phoneNumberRegex = /^\d{11}$/;
        return phoneNumberRegex.test(phoneNumber);
      }
      //#endregion

      //#region Enable The Register BUTTON 
      registerEnable():boolean{
      return this.isValidEmail(this.user.email) && this.isValidPassword(this.user.password) &&
      
          this.isValidConfirmPassword(this.user.password,this.user.confirmPassword)
          &&
          this.isValidPhoneNumber(this.user.mobile)
          &&
          this.user.gender != null 
          && 
          this.user.name !=null;
          
      }
      //#endregion
}
