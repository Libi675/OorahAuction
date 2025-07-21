import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { User } from '../models/user.models';
import { Login } from '../models/login.models';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  BASE_URL = 'http://localhost:5000';
  http: HttpClient = inject(HttpClient);
  //
  roleSubject = new BehaviorSubject<string>('noToken');
  public role$: Observable<string> = this.roleSubject.asObservable();
 
  constructor() { }
  addUser(user: User) {
    const newObj = {
      email: user.email,
      password: user.password,
      firstName: user.firstName,
      lastName: user.lastName,
      city: user.city,
      phone: user.phone,
    };
    console.log("user", newObj);
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<User>(this.BASE_URL + "/Register", newObj, { headers })
  }
  loginUser(LoginUser: Login) {
    const loginuser = {
      email: LoginUser.email,
      password: LoginUser.password,
    };
    return this.http.post<any>(this.BASE_URL + "/Login", loginuser,{ responseType: 'text' as 'json', withCredentials: true})
  }
  
  updateRole(): void {
    this.getRoleByToken().subscribe(role => {
      this.roleSubject.next(role);
    });
  }
  


  logout() {
    return this.http.post(this.BASE_URL + "/Logout", {}, { withCredentials: true });
  }
  getRoleByToken(): Observable<string> {
    return this.http.get(this.BASE_URL + "/getRoleByToken", { responseType: 'text', withCredentials: true });
}


}