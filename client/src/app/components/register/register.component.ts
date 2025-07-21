import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { User } from '../../models/user.models';
import { AuthService } from '../../servises/auth.service';
import { ActivatedRoute, RouterModule, RouterOutlet } from '@angular/router';
import {  Router } from '@angular/router';
import { DonorListComponent } from '../donor-list/donor-list.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule , FormsModule, CommonModule, MatFormFieldModule,
       MatInputModule,MatButtonModule , MatDividerModule, MatIconModule,RouterOutlet,ReactiveFormsModule
       ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  router: Router = inject(Router);
  user : User = new User()
  userSrv: AuthService = inject(AuthService); 
  
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  nameFormControl = new FormControl('', [Validators.required]);
  phoneFormControl = new FormControl('', [Validators.required, Validators.minLength(9)]);
  passwordFormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  mailFormControl=new FormControl('', [Validators.required, Validators.email,]); 

  addNewUser( user:User){
    this.userSrv.addUser(user).subscribe((newUser:User)=>{
      alert("נוספת בהצלחה למערכת!שלום 🎁🎀"+newUser.firstName)
      this.user=new User()
      this.router.navigateByUrl('/app-home/login');
     

    },(error:Error)=>{
      console.log(error.message);     
    }
  );
  }
}

