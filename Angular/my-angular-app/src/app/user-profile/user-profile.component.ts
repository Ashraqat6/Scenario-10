import { Component, OnInit } from '@angular/core';
import { Client } from '../_models/Client';
import { AuthenService } from '../Service/authen.service';
import { ActivatedRoute } from '@angular/router';
import { Species } from '../_models/Species';
import { SpeciesService } from '../Service/species.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  client: Client | null = null;
  userId: string = "";

  constructor(
    private userService: AuthenService,
    private activateRoute: ActivatedRoute,
    private speciesService: SpeciesService
  ) {}

  ngOnInit(): void {
    this.userId = this.activateRoute.snapshot.params['id'];
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.userService.UserDetails(this.userId).subscribe((data) => {
      this.client = data;
      this.fetchAndAssignSpeciesNames();
    });
  }

  fetchAndAssignSpeciesNames(): void {
    const observables = this.client?.reports.map((report) =>
      this.speciesService.getSpecies(report.speciesId)
    );

    if (observables) {
      forkJoin(observables).subscribe(
        (speciesList: Species[]) => {
          this.updateSpeciesNames(speciesList);
        },
        (error) => {
          console.log("Error fetching species: ", error);
        }
      );
    }
  }

  updateSpeciesNames(speciesList: Species[]): void {
    speciesList.forEach((species, i) => {
      this.client!.reports[i].speciesName = species.name;
    });
  }
}
