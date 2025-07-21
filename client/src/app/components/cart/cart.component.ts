import { Component, inject } from '@angular/core';
import { OrdersCartService } from '../../servises/orders-cart.service';
import { Order } from '../../models/order.models';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddGiftComponent } from '../add-gift/add-gift.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [FormsModule, CommonModule,MatTableModule,MatButtonModule, MatDividerModule, MatIconModule
      ,FormsModule, MatInputModule, ReactiveFormsModule,MatCardModule, MatButtonModule],
      
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  OrderListDB:Order[]=[]
  OrderSrv: OrdersCartService = inject(OrdersCartService);
  router: Router = inject(Router);
  
 ngOnInit(){
    this.getOrdersCart()
  }
  getOrdersCart(){
    this.OrderSrv.getCart().subscribe((orders)=>{ this.OrderListDB = orders;
      console.log('Orders loaded✔️✅:', this.OrderListDB);})
  }
  deleteOrder(orderId:number){
    this.OrderSrv.deleteFromCart(orderId).subscribe(()=>{this.getOrdersCart(),
      console.log("delete",this.OrderListDB);
      
    })
  }
  payment(){
    this.OrderSrv.toBuy().subscribe(()=>{
    alert("you buy!!!!!!!!!"),
    this.router.navigateByUrl('/app-home/payment'); 
    })

  }
 
  

}
