import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Species } from '../_models/Species';

@Injectable({
  providedIn: 'root'
})
export class SpeciesService {
  baseurl:string="https://localhost:7156/api/Species/"
  
  getAll(){
    return this.http.get<Species[]>(this.baseurl)
  }
  getSpecies(id:number)
  {
    return this.http.get<Species>(this.baseurl+id)
  }
  updateSpecies(Species:Species){
    return this.http.put<Species>(this.baseurl+Species.id,Species)
  }
  deleteSpecies(id:string){
    return this.http.delete(this.baseurl+id);
  }
  constructor(public http:HttpClient) { }
}