import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageService } from '../Service/image.service';
import { ReportsService } from '../Service/reports.service';
import { Report } from '../_models/Report';

@Component({
  selector: 'app-sight-report',
  templateUrl: './sight-report.component.html',
  styleUrls: ['./sight-report.component.css']
})
export class SightReportComponent implements OnInit {
  imageUrl = '';
  addReportFrm: FormGroup;

  item!: Report;
  currentSpeciesId: number=0;

  constructor(
    private fb: FormBuilder,
    public reportService: ReportsService,
    private imgService: ImageService,
    private activateRoute: ActivatedRoute,
    private router: Router
  ) {
    this.addReportFrm = this.fb.group({
      speciesId: '',      
      location: ['', [this.customPatternValidator('[A-Za-zء-ي _ , ، \'-]{3,500}')]],     
      date:'',
      img: '',
    });
  }
  customPatternValidator(pattern: string) {
    return (control: any) => {
      const regex = new RegExp(pattern);
      const isValid = regex.test(control.value);
      return isValid ? null : { pattern: control.value };
    };
  }
  ngOnInit(): void {
    this.activateRoute.params.subscribe((params) => {
      this.currentSpeciesId = params['id'];
    });
  }

  public uploadPhoto(e: Event) {
    const input = e.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;

    this.imgService.upload(file).subscribe((response) => {
      this.imageUrl = response.url;
      this.addReportFrm.patchValue({ img: this.imageUrl });
    });
  }

  addItem(e: Event) {
    e.preventDefault();

    console.log(this.addReportFrm.value);
    this.item = {
      ...this.addReportFrm.value,
      userId:localStorage.getItem('id'),
      speciesId: this.currentSpeciesId,
    };
    console.log(this.item);
    
    this.reportService.addReport(this.item).subscribe((res) => {
      console.log('your report has been added Successfully');
      this.router.navigate(['/home']);
    });
  }
  resetForm() {
    this.addReportFrm.reset();
  }

  onBack(): void {
    this.addReportFrm.reset();
    this.router.navigate(['/home']);
  }


  get location() {
    return this.addReportFrm.get('location');
  }


  get date() {
    return this.addReportFrm.get('date');
  }
}

