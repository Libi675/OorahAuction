import { Component, Input, Output, inject,EventEmitter } from '@angular/core';
import { GiftService } from '../../servises/gift.service';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Gift } from '../../models/gift.models';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';


import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { error } from 'console';
import { Categorya } from '../../models/category.models';
import { Donor } from '../../models/donor.models';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-add-gift',
  standalone: true,
  
  imports: [ MatFormFieldModule,MatFormFieldModule
    ,FormsModule, CommonModule, MatFormFieldModule,
     MatInputModule,MatButtonModule, MatDividerModule,
      MatIconModule,MatFormFieldModule,
      MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule],
  templateUrl: './add-gift.component.html',
  styleUrl: './add-gift.component.css'
})
export class AddGiftComponent {
  GiftSrv: GiftService = inject(GiftService); 
  g : Gift = new Gift()
  router: Router = inject(Router);
  categories: Categorya[] = [];
  donors: Donor[] = [];
  textFormControl = new FormControl('', [Validators.required]);
  nameFormControl = new FormControl('', [Validators.required]);
  inputNumFormControl = new FormControl('', [Validators.required, Validators.max(40),Validators.min(10)]);
  ticketPrice = new FormControl(0, [Validators.required, Validators.max(40),Validators.min(10)]);

  //להעביר את המתנה החדשה לאבא
  @Output() giftAdd = new EventEmitter<Gift>()
  errorMessage: string | null = null;
  toppings = new FormControl('');

  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];

  ngOnInit() {
    this.loadCategories();
    this.loadDonors();
  }
  loadCategories() {
    this.GiftSrv.getCategory().subscribe({
      next: (data) => {
        this.categories = data;
        console.log("ccc",this.categories);
        
      },
      error: (err) => {
        console.error('שגיאה בטעינת קטגוריות:', err);
      },
    });
  }

  loadDonors() {
    this.GiftSrv.getDonor().subscribe({
      next: (data) => {
        this.donors = data;
        console.log("ddd",this.donors);
      },
      error: (err) => {
        console.error('שגיאה בטעינת תורמים:', err);
      },
    });
  }




  addNewGift(g:Gift) {  
    this.GiftSrv.addGift(g).subscribe({next:(newGift:Gift)=>{
      this.giftAdd.emit(newGift);
      this.g=new Gift()
      this.router.navigateByUrl('/app-home/gift');
    },error:(error)=>{
             console.error('אין הזמנות:', error);
           alert('you nead to enter uniqe giftname\n and donor that exsit😭😢😱\n');
         }});
   
  }}
