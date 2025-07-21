import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { Donor } from '../../models/donor.models';
import { DonorService } from '../../servises/donor.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-add-donor',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatDividerModule, MatIconModule
    , FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './add-donor.component.html',
  styleUrl: './add-donor.component.css'
})
export class AddDonorComponent {
  nameFormControl = new FormControl('', [Validators.required]);
  phoneFormControl = new FormControl('', [Validators.required, Validators.minLength(9)]);
  passwordFormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  mailFormControl=new FormControl('', [Validators.required, Validators.email,]); 
  dataSource :Donor[]=[];
  router: Router = inject(Router);
  donor:Donor=new Donor()
  DonorSrv: DonorService = inject(DonorService); 
   saveDonor(donoToAdd:Donor){
    this.DonorSrv.addDonor(donoToAdd).subscribe(
      (newDonor:Donor)=>{
        console.log("donor",newDonor) 
        this.donor=new Donor ();
        this.router.navigateByUrl('/app-home/donor');
      }

    )
    
   }

}
