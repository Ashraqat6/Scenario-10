import { Component, OnDestroy, OnInit } from '@angular/core';
import { SpeciesService } from '../Service/species.service';
import { Species } from '../_models/Species';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenService } from '../Service/authen.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  constructor(public speciesService: SpeciesService,
     private route: Router, private authService:AuthenService) {}
  species: Species[] = [];
  isUser=false;
  filteredSpecies: Species[] = [];
  sub: Subscription | null = null;

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
  ngOnInit(): void {
    this.authService.isAuth$.subscribe((value)=>{this.isUser=value;}
    )
    this.sub = this.speciesService.getAll().subscribe((data) => {
      this.species = data;
      this.filteredSpecies = data;
    });
  }
  checkAuth(itemId: number) {
    if (this.isUser==false) {
      this.route.navigateByUrl('/login');
    } else {
      this.route.navigateByUrl('/sightReport/' + itemId);
    }
  }
  onSearch(event: Event): void {
    const query = (event.target as HTMLInputElement).value;
    this.filteredSpecies = this.species.filter((item) =>
      item.name.toLowerCase().includes(query.toLowerCase())
    );
  }
  clearSearch(): void {
    (document.querySelector('input[type="search"]') as HTMLInputElement).value = '';
    this.filteredSpecies = this.species;
  }
  
}
