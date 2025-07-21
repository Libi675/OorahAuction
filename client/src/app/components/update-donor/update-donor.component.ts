import { Component, inject } from '@angular/core';
import { Donor } from '../../models/donor.models';
import { DonorService } from '../../servises/donor.service';

import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-update-donor',
  standalone: true,
  imports: [FormsModule, CommonModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDividerModule, MatIconModule, CommonModule, MatTableModule, MatButtonModule, MatDividerModule, MatIconModule
    , FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './update-donor.component.html',
  styleUrl: './update-donor.component.css'
})
export class UpdateDonorComponent {
  nameFormControl = new FormControl('', [Validators.required]);
  phoneFormControl = new FormControl('', [Validators.required, Validators.minLength(9)]);
  passwordFormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  mailFormControl=new FormControl('', [Validators.required, Validators.email,]); 
  dataSource :Donor[]=[];

  router: Router = inject(Router);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  selectedId: number = -1;
  donor: Donor = new Donor()
  DonorSrv: DonorService = inject(DonorService);
  //מציאת התורם הנוכחי ואיתחות הטופס בתורם הנוכחי
  ngOnInit() {
    this.activatedRoute.params.subscribe(p => {
      if (p['id'] > 0) {
        this.selectedId = p['id'];
        console.log("id", this.selectedId);
        this.getDonor()

      }
    })
  }
  getDonor() {
    this.DonorSrv.getDonorById(this.selectedId).subscribe((donorUp: Donor) => {
    console.log("the donor to update", donorUp);
    this.donor=donorUp
    })
  }

  saveDonor(DonorToUpdate: Donor) {
    DonorToUpdate.donorId = this.selectedId
    this.DonorSrv.updateDonor(DonorToUpdate).subscribe(
      (newDonor: Donor) => {
        console.log("donorUp", newDonor)
        this.donor = new Donor();
        this.router.navigateByUrl('/app-home/donor');
      }
    )
  }
}
