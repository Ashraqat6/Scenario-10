import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenService } from 'src/app/Service/authen.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  constructor(private authService : AuthenService, private route: Router) {}

isLogged = false ;
userId:string | null = null;

ngOnInit(): void {
  this.checkIfUserLoggedIn();
  this.checkforUserId();
  this.authService.isAuth$.subscribe((value)=>{this.isLogged=value;});
  this.userId=localStorage.getItem('id');
  this.authService.userIdSubject.subscribe((userId) => {
    this.userId = userId;
  });
}
Logout(){
  this.authService.logout();
  this.route.navigateByUrl('/home');
}
checkIfUserLoggedIn() {
  if (localStorage.getItem('token')) {
    this.authService.isAuth$.next(true);
  }
}
checkforUserId() {
  if (localStorage.getItem('id')) {
    this.authService.userIdSubject.next(localStorage.getItem('id'));
  }
}

}
