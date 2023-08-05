import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ReportsService } from '../Service/reports.service';
import { Report } from '../_models/Report';
import { SpeciesService } from '../Service/species.service';
import { Species } from '../_models/Species';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit, OnDestroy{

  sub: Subscription | null = null;
  reports:Report[] = [];
  specie:Species| null = null;
  searchInput: string = '';

  constructor(public ReportServices: ReportsService,public router:Router,public speciesService: SpeciesService) {
}

ngOnInit(): void {
  this.loadReports();
  }
ngOnDestroy(): void {
  this.sub?.unsubscribe();
}
loadReports() {
  this.sub = this.ReportServices.getAll().subscribe(response => {
    this.reports = response;
    for (let i = 0; i < this.reports.length; i++) {
      this.speciesService.getSpecies(this.reports[i].speciesId).subscribe(
        (species: Species) => {
          this.specie = species;
          this.reports[i].speciesName = this.specie?.name;
        },
        (error) => {
          console.log("Error fetching species: ", error);
        }
      );
    }
    console.log(this.reports);
  })
}
 // Function to handle search
 onSearch(event: Event) {
  const query = (event.target as HTMLInputElement).value;
  this.searchInput = query;
  this.filterReports();
}

// Function to clear search and reload all reports
clearSearch() {
  (document.querySelector('input[type="search"]') as HTMLInputElement).value = '';
  this.searchInput='';
  this.filterReports();
}

// Function to filter reports based on the search input
filterReports() {
  if (!this.searchInput) {
    // If search input is empty, reload all reports
    this.loadReports();
  } else {
    // If search input is not empty, filter reports by species name
    this.reports = this.reports.filter(report => report.speciesName.toLowerCase().includes(this.searchInput.toLowerCase()));
  }
}
}

