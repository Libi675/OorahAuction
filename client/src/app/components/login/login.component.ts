import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormControl, FormsModule, Validators } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from '../../servises/auth.service';
import { User } from '../../models/user.models';
import { MatButtonModule } from '@angular/material/button';
import { Login } from '../../models/login.models';
import { ReactiveFormsModule,  } from '@angular/forms';
import { log } from 'console';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule , FormsModule, CommonModule, MatFormFieldModule,
     MatInputModule,MatButtonModule , MatDividerModule, MatIconModule,RouterOutlet,ReactiveFormsModule
     ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  router: Router = inject(Router);
  user : Login = new Login()
  // email:string="";
  // password:string="";
  userSrv: AuthService = inject(AuthService); 
  
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  nameFormControl = new FormControl('', [Validators.required, Validators.maxLength(22),Validators.minLength(2)]);
  inputNumFormControl = new FormControl('', [Validators.required, Validators.max(40),Validators.min(10)]);
  passwordFormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  mailFormControl=new FormControl('', [Validators.required, Validators.email,]);  
  login(user:Login){
    this.userSrv.loginUser(user).subscribe({ next:(ans: string)=>{
      this.userSrv.updateRole(); 
      console.log('User logged in, role updated:', ans);
      this.router.navigateByUrl('/app-home/giftUser/0');
  },error:(error)=>{
    console.log(error);
    alert("your details are worng!\n please try again!")
    this.router.navigateByUrl('/app-home/register');
  }
  })
 

};
}