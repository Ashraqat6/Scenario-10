import { RegisterDTO } from '../_models/RegisterDTO';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {


  registerUser(user: RegisterDTO) {
    return this.http.post('https://localhost:7156/api/User/register', user);
  }

  constructor(public http:HttpClient) { }
}

