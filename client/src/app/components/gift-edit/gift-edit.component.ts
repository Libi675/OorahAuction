import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Gift } from '../../models/gift.models';
import { GiftService } from '../../servises/gift.service';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { Categorya } from '../../models/category.models';
import { Donor } from '../../models/donor.models';

@Component({
  selector: 'app-gift-edit',
  standalone: true,
  imports: [FormsModule, CommonModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDividerModule, MatIconModule, CommonModule, MatTableModule, MatButtonModule, MatDividerModule, MatIconModule
    , FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule,
    MatFormFieldModule, MatSelectModule, FormsModule, ReactiveFormsModule],
  templateUrl: './gift-edit.component.html',
  styleUrl: './gift-edit.component.css'
})
export class GiftEditComponent {
  // nameFormControl = new FormControl('', [Validators.required, Validators.maxLength(22),Validators.minLength(2)]);
  // whightFormControl = new FormControl('', [Validators.required, Validators.maxLength(22),Validators.minLength(2)]);
  // inputNumFormControl = new FormControl('', [Validators.required, Validators.max(40)]);
  imageFormControl=new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(2)]);
  categoyFormControl=new FormControl(0, [Validators.required, Validators.maxLength(200), Validators.minLength(2)]);
  nameFormControl = new FormControl('', [Validators.required]);
 imgFormControl = new FormControl('', [Validators.required]);
  inputNumFormControl = new FormControl('', [Validators.required, Validators.max(40),Validators.min(10)]);
  ticketPrice = new FormControl(0, [Validators.required, Validators.max(40),Validators.min(10)]);
  router: Router = inject(Router);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  selectedId: number = -1;
  Gift: Gift = new Gift()
  GiftSrv: GiftService = inject(GiftService);
  categories: Categorya[] = [];
  donors: Donor[] = [];
  ngOnInit() {
    this.loadCategories();
    this.loadDonors();
    this.activatedRoute.params.subscribe(p => {
      if (p['id'] > 0) {
        this.selectedId = p['id'];
        console.log("id", this.selectedId);
        this.getGift()
      }
    })
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


  getGift() {
    this.GiftSrv.getGiftById(this.selectedId).subscribe((GiftUp: Gift) => {
    console.log("the Gift to update", GiftUp);
    this.Gift=GiftUp
    })
  }

  saveGift(GiftToUpdate: Gift) {
    GiftToUpdate.giftId = this.selectedId
    this.GiftSrv.updateGift(GiftToUpdate).subscribe(
      (newGift: Gift) => {
        console.log("GiftUp", newGift)
        this.Gift = new Gift();
        this.router.navigateByUrl('/app-home/gift');
      }
    )
  }
}
