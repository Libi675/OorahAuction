import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../servises/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatTabsModule, RouterOutlet, MatTabsModule, MatButtonModule, RouterLink, RouterLinkActive, FormsModule, CommonModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDividerModule, MatIconModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',

})
export class HomeComponent {
  authSrv: AuthService = inject(AuthService);
  router: Router = inject(Router);
  parmToUser: number = 0
  roleByToken: string = 'noToken';

  ngOnInit() {
    // מאזין לשינויים ב-role
    this.authSrv.role$.subscribe((role) => {
      this.roleByToken = role;
      console.log('Updated role:', this.roleByToken);
    });    
    this.authSrv.updateRole(); 
  }


  logout() {
    this.authSrv.logout().subscribe({
      next: () => {
        this.authSrv.roleSubject.next('noToken'); 
        this.router.navigateByUrl('/app-home/login');
        console.log("logout");

      },
      error: (err) => {
        console.error('Logout failed:', err);
        console.log("not-logout");
      },
    });
  }
}

