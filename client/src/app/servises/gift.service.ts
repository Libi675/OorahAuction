import {   Injectable, Signal, inject, signal } from '@angular/core';
import { Gift } from '../models/gift.models';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { response } from 'express';
import { User } from '../models/user.models';
import { Categorya } from '../models/category.models';
import { Donor } from '../models/donor.models';

@Injectable({providedIn: 'root'})


export class GiftService {
  BASE_URL = 'http://localhost:5000/api/Gift';
  BASE='http://localhost:5000/'
  http: HttpClient = inject(HttpClient);
  constructor() { }
  //listDonor: Observable<Gift[]>=Observable<>
  giftList:Gift[]=[]

  countGift:number=0
  //we ty to do signal😭😭😭😭
  // raffledGifts: Signal<Set<number>> = new Signal(new Set<number>());

  getAllGifts():  Observable<Gift[]> {
    return this.http.get<Gift[]>(this.BASE_URL,{ withCredentials: true});
  }
  getAllGiftsSort():  Observable<Gift[]> {
    return this.http.get<Gift[]>(this.BASE+"getPopular",{ withCredentials: true});
  }
  getCategory():Observable<Categorya[]>{
    return this.http.get<Categorya[]>(this.BASE+"api/Category",{withCredentials: true})
  }
  getDonor():Observable<Donor[]>{
    return this.http.get<Donor[]>(this.BASE+"api/Donor",{withCredentials: true})
  }
  Profit(): Observable<number> {
    return this.http.get<number>(this.BASE + 'GetProfit', { withCredentials: true });
  }
  //
  addGift(g: Gift):Observable<Gift> {
    const newObj = {
      giftName: g.giftName,
      ticketPrice:g.ticketPrice,
      donorId:g.donorId,
      categoryaId:g.categoryaId,
      image:g.image
    };
    console.log("ccc",newObj);
    const headers = { 'Content-Type': 'application/json' };
  return this.http.post<Gift>(this.BASE_URL,newObj,{ withCredentials: true});
  }
  // pdateDonor(d: Donor): Observable<Donor> {
  //   console.log("serviceDonor:", d);
  //   const newObj = {
  //     fullName: d.fullName,
  //     email: d.email,
  //     phone: d.phone,

  //   };
  //   console.log("donor", newObj);
  //   return this.http.put<Donor>(this.BASE_URL + '/' + d.donorId, newObj, { withCredentials: true })
  // }
  updateGift(g: Gift):Observable<Gift> {
    let id=g.giftId
    console.log("service:",g);
    console.log("id",id);
    
    const newObj = {
      giftName: g.giftName,
      ticketPrice:g.ticketPrice,
      donorId:g.donorId,
      categoryaId:g.categoryaId,
      image:g.image
    };
    return this.http.put<Gift>(this.BASE_URL+'/'+id,newObj,{ withCredentials: true})
  }

  deleteGift(id: number):Observable<void> {
   
    let thisGift = this.giftList.findIndex(x => x.giftId == id);
    if (thisGift>= 0)
      this.giftList.splice(thisGift, 1);
    
    return this.http.delete<void>(this.BASE_URL+'/'+id,{ withCredentials: true})

  }
  getNameDonor(id:Number) :Observable<string>{
    return this.http.get<string>(this.BASE_URL+'/NameDonor/'+id,{ withCredentials: true});
  }
  searchGift(word:string){
    return this.http.get<Gift[]>(this.BASE+'searchGiftBy'+word,{ withCredentials: true});
  }
  getGiftById(id:number) :Observable<Gift> {
    return this.http.get<Gift>(this.BASE_URL + '/' + id, { withCredentials: true })
  }
 
  ruffleGift(id:number) :Observable<User> {
    return this.http.get<User>(this.BASE + 'ruffle/' + id, { withCredentials: true })
  }

}
