import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Report } from '../_models/Report';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  baseurl:string="https://localhost:7156/api/Reports/"
  
  getAll(){
    return this.http.get<Report[]>(this.baseurl)
  }
  addReport(report:Report){
    return this.http.post<Report>(this.baseurl,report)
  }
  getReport(id:string)
  {
    return this.http.get<Report>(this.baseurl+id)
  }
  updateReport(report:Report){
    return this.http.put<Report>(this.baseurl+report.id,report)
  }
  deleteReport(id:string){
    return this.http.delete(this.baseurl+id);
  }
  
  constructor(public http:HttpClient) { }
}