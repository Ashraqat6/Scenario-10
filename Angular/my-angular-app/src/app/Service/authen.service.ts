import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginDTO } from '../_models/LoginDTO';
import { TokenDTO } from '../_models/TokenDTO';
import { Client } from '../_models/Client';
@Injectable({
  providedIn: 'root'
})
export class AuthenService {
  
  public isAuth$ = new BehaviorSubject<boolean>(false);
  public userIdSubject = new BehaviorSubject<string | null>(null);

  constructor(private http: HttpClient) {}

  UserDetails(id:String) {
    return this.http.get<Client>('https://localhost:7156/api/User/'+ id);
  }

  login(credentials: LoginDTO): Observable<TokenDTO> {
    return this.http
      .post<TokenDTO>('https://localhost:7156/api/User/login', credentials)
      .pipe(
        tap((tokenDto) => {
          localStorage.setItem('token', tokenDto.token);
          localStorage.setItem('id', tokenDto.id);
          this.isAuth$.next(true);
          const userId = localStorage.getItem('id');
          this.userIdSubject.next(userId);
        }),
        tap({
          error: () => {
            alert('Wrong email or password');
          }
        })
      ); 
  }

  public logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('id');
    this.userIdSubject.next(null);
    this.isAuth$.next(false);
  }
}
