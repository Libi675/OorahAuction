
import { Component, effect, inject } from '@angular/core';
import { Gift } from '../../models/gift.models';
import { GiftService } from '../../servises/gift.service';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddGiftComponent } from '../add-gift/add-gift.component';
import {MatTableModule} from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import { error, log } from 'console';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {NgModule} from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import { OrdersCartService } from '../../servises/orders-cart.service';
import { GiftListComponent } from '../gift-list/gift-list.component';
import { ActivatedRoute } from '@angular/router';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
  btn:string;
}
@Component({
  selector: 'app-gift-to-user',
  standalone: true,
  imports: [GiftListComponent,FormsModule, CommonModule,AddGiftComponent,MatTableModule,MatButtonModule, MatDividerModule, MatIconModule
    ,FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule,MatCardModule, MatButtonModule],
  templateUrl: './gift-to-user.component.html',
  styleUrl: './gift-to-user.component.css'
})
export class GiftToUserComponent {
  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol','btn','img'];
  raffledGifts: Set<number> = new Set();
  dataSource :Gift[]=[];
  flag : boolean = false; 
  g : Gift = new Gift();
  GiftSrv: GiftService = inject(GiftService);
  OrderSrv: OrdersCartService = inject(OrdersCartService);
   activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  giftListDB:Gift[]=[]
  prm:number=0
  //רשימת המתנות שלי
  listGift=this.GiftSrv.getAllGifts().subscribe(gifts => {
    this.giftListDB = gifts; // שמירת הנתונים במערך
    console.log('Gifts loaded:', this.giftListDB);
  })
  constructor() {
    // האזנה לשינויים ב-Signal של המתנות שהוגרלו
   
  }
  
 ngOnInit(){
    this.getAll()
    this.activatedRoute.params.subscribe(p => {
     
        this.prm = p['parm'];
        console.log("iddd", this.prm);
      
    })
  }
  search(str:string){
    if(str=="")this.ngOnInit();
    else{
      this.GiftSrv.searchGift(str).subscribe(data=>{
        this.giftListDB =data
        console.log(this.giftListDB);
      });
    }}
  getAll(){
    this.GiftSrv.getAllGifts().subscribe(data=>{
      this.dataSource =data
      console.log(this.dataSource);
    });
    console.log("getAll");
  }
  
  addToCart(giftId:number):void {
    this.OrderSrv.addToCart(giftId).subscribe((orders)=>{
      console.log('Order updated:', orders);
      // this.OrderListDB = updatedOrders; 
    })
  }
}
