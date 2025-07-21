// import { HttpClient, HttpHeaders } from '@andular/common/http';
// import { Injectable, inject } from '@andular/core';
import {   Injectable, inject } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { response } from 'express';
import { Donor } from '../models/donor.models';

@Injectable({
  providedIn: 'root'
})
export class DonorService {
  BASE_URL = 'http://localhost:5000/api/Donor';
  http: HttpClient = inject(HttpClient);
  constructor() { }
  DonorList: Donor[] = []

  getAllDonors(): Observable<Donor[]> {
    return this.http.get<Donor[]>(this.BASE_URL, { withCredentials: true });

  }
  addDonor(d: Donor): Observable<Donor> {
    const newObj = {
      fullName: d.fullName,
      email: d.email,
      phone: d.phone,

    };
    console.log("donor", newObj);
    //const headers = { 'Content-Type': 'application/json' };
    return this.http.post<Donor>(this.BASE_URL, newObj, { withCredentials: true });
  }
  updateDonor(d: Donor): Observable<Donor> {
    console.log("serviceDonor:", d);
    const newObj = {
      fullName: d.fullName,
      email: d.email,
      phone: d.phone,

    };
    console.log("donor", newObj);
    return this.http.put<Donor>(this.BASE_URL + '/' + d.donorId, newObj, { withCredentials: true })
  }

  deleteDonor(id:number): Observable<void> {

    let thisDonor = this.DonorList.findIndex(x => x.donorId == id);
    if (thisDonor >= 0)
      this.DonorList.splice(thisDonor, 1);

    return this.http.delete<void>(this.BASE_URL + '/' + id, { withCredentials: true })

  }
  getDonorById(id:number) :Observable<Donor> {
    return this.http.get<Donor>(this.BASE_URL + '/' + id, { withCredentials: true })
  }


}
