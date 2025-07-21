import { Component, inject } from '@angular/core';
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
import { MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { User } from '../../models/user.models';
import { Signal } from '@angular/core'; 



export interface PeriodicElement {
  name: string;
  
  position: number;
  weight: number;
  symbol: string;
  btn:string;
  winner:string
}

@Component({
  selector: 'app-gift-list',
  standalone: true,
  imports: [FormsModule, CommonModule,AddGiftComponent,MatTableModule,MatButtonModule, MatDividerModule, MatIconModule
    ,FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatDialogTitle]
  ,
  templateUrl: './gift-list.component.html',
  styleUrl: './gift-list.component.css'
})

export class GiftListComponent {
  nameFormControl = new FormControl('', [Validators.required, Validators.maxLength(22),Validators.minLength(2)]);
  whightFormControl = new FormControl('', [Validators.required, Validators.maxLength(22),Validators.minLength(2)]);
  inputNumFormControl = new FormControl('', [Validators.required, Validators.max(40),Validators.min(10)]);
  categoyFormControl=new FormControl(0, [Validators.required, Validators.maxLength(200), Validators.minLength(1)])
   displayedColumns: string[] = ['position', 'name', 'weight', 'symbol','img','cat',"winner"];
  dataSource :Gift[]=[];
  u : User = new User();
  flag : boolean = false; 
  flagProfit : boolean = false;
  date: number = new Date().getTime();
  today: Date = new Date();

  g : Gift = new Gift();
  router: Router = inject(Router);
  readonly dialog = inject(MatDialog);
  
  giftToUpdate : Gift | null = null;
  sum:number=0;

  
  

  
  GiftSrv: GiftService = inject(GiftService);
  giftListDB:Gift[]=[]
  listWinnerGift:number[]=[]
  ind:number=0
  winners: { [giftId: number]: string } = {};
  //רשימת המתנות שלי
  listGift=this.GiftSrv.getAllGifts().subscribe(gifts => {
    this.giftListDB = gifts; // שמירת הנתונים במערך
    console.log('Gifts loaded:', this.giftListDB);
  })
 ngOnInit(){
    this.getAll()
    console.log("😋😊😉",this.g.donorsGift.fullName);
    
  }
  // קים מוצר לכן ידלק הטופס בעת לחיצה אני שומרת את אוביקט מתנה לעידכון
 
  getProfit(flag:boolean){
  this.GiftSrv.Profit().subscribe((sumProfit)=>{
   this.sum=sumProfit,
   console.log("profit", this.sum);
   

  })
  this.flagProfit=flag
  }
  closeProfit(){
    this.flagProfit=false;
  }
  saveGift(giftTo:Gift) {
    this.GiftSrv.updateGift(giftTo).subscribe(
      (newGift:Gift) => {
        console.log("newGift",newGift);
        this.giftToUpdate = null;
        this.getAll()
      }
      
    );
  }
  
  ruffle(giftId:number){
     this.GiftSrv.ruffleGift(giftId).subscribe({next:(user:User)=>{
      this.u=user,
      this.winners[giftId] = `${user.firstName} ${user.lastName}`; 
      console.log("Winner:", this.winners[giftId]);
      alert("User " + this.winners[giftId] + " is the winner! 🎉🎉🎉");
    



     },
    error:(error)=>{
      console.error('אין הזמנות:', error);
      alert('this gift havent orders😭😢😱');
    }
    });
    
  }
  navigateUpdate(prm:number){
    this.router.navigateByUrl('/app-home/giftUser/'+prm);
  
  }
  

  deleteG(id:number){

    this.GiftSrv.deleteGift(id).subscribe(data=>{this.dataSource=this.dataSource.filter(gift=>gift.giftId!=id)
    console.log('Gift delet');
      
    })
    console.log("delete");
  } 
 
  cancelEdit() {
    this.giftToUpdate = null; // ביטול העריכה
  }
  
  addGiftForm() {
    this.flag=true;
    
    
  }
  onGiftAdded(gift:Gift){
    this.getAll()
    this.flag = false; // סגירת טופס ההוספה
    
  }
  search(str:string){
  if(str=="")this.ngOnInit();
  else{
    this.GiftSrv.searchGift(str).subscribe(data=>{
      this.dataSource =data
      console.log(this.dataSource);
    });
  }
   
  }
  getAll(){
    this.GiftSrv.getAllGifts().subscribe(data=>{
      this.dataSource =data
      console.log(this.dataSource);
    });
    console.log("getAll");
  }
  sortGift(){
    this.GiftSrv.getAllGiftsSort().subscribe(data=>{
      this.dataSource =data
      console.log(this.dataSource);
    });
    console.log("🎆🎇🎆🎇sort");
  
  }
  closeSort(){
    this.getAll()
  }
 
  openDialog(gift:Gift){
    this.dialog.open(AddGiftComponent);
  }
  navigateGift() {
    this.router.navigateByUrl('/app-home/gift/add');
  }
  editGift(gift: Gift) {
    this.giftToUpdate = {...gift} ; // יצירת עותק לעריכה
    this.router.navigateByUrl('/app-home/gift/update/'+gift.giftId);
  }

}
