import { Component, inject } from '@angular/core';
import { Donor } from '../../models/donor.models';
import { DonorService } from '../../servises/donor.service';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AddGiftComponent } from '../add-gift/add-gift.component';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import {
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle,
} from '@angular/material/dialog';
@Component({
  selector: 'app-donor-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatDividerModule, MatIconModule
    , FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatDialogTitle,],
  templateUrl: './donor-list.component.html',
  styleUrl: './donor-list.component.css'
})
export class DonorListComponent {
  displayedColumns: string[] = [ 'btn','position', 'name', 'weight', 'symbol'];
  dataSource: Donor[] = [];
  flag: boolean = false;
  d: Donor = new Donor();
  // readonly dialog = inject(MatDialog);
  DonorToUpdate: Donor | null = null;
  router: Router = inject(Router);
  DonorSrv: DonorService = inject(DonorService);
  DonorListDB: Donor[] = []
  ngOnInit() {
    console.log("en");
    
    this.getAll()

  }
  //רשימת התורמים שלי
  listDonor = this.DonorSrv.getAllDonors().subscribe(Donors => {
    this.DonorListDB = Donors; // שמירת הנתונים במערך
    console.log('Donors loaded:', this.DonorListDB);
  })
 
  getAll() {
    this.DonorSrv.getAllDonors().subscribe(data => {
      this.dataSource = data
      console.log(this.dataSource);
    });
    console.log("getAllDonor");
  }
  navigateDonor() {
    this.router.navigateByUrl('/app-home/donor/add');
  }
  editDonor(id:number){
    this.router.navigateByUrl('/app-home/donor/update/'+id);
  }
  
  deleteDonor(id:number){
    this.DonorSrv.deleteDonor(id).subscribe(data=>{this.dataSource=this.dataSource.filter(donor=>donor.donorId!=id);
      console.log('Donor delet');
        
      })
      console.log("delete");
  }
}
