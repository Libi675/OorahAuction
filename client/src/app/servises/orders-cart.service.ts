import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Order } from '../models/order.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrdersCartService {

  constructor() { }
  BASE_URL = 'http://localhost:5000/api/Order/';
  BASE='http://localhost:5000/'
  http: HttpClient = inject(HttpClient);

  addToCart(giftId:number):Observable<Order[]>{
    return this.http.post<Order[]>(this.BASE+'addToCart/'+giftId,{},{ withCredentials: true});
  }
  toBuy():Observable<any>{
      return this.http.put<string>(this.BASE+"toBuy",{},{ withCredentials: true})
  }
  deleteFromCart(id:number):Observable<string>{
   return this.http.delete<string>(this.BASE_URL+id, { withCredentials: true })
  }
  getCart(): Observable<Order[]> {
    return this.http.get<Order[]>(this.BASE + 'getUserOrder', { withCredentials: true });
  }
 

}
