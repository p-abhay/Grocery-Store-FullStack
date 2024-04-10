import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user.model';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  private baseApiUrl : string = environment.baseApiURL;
  private userPayLoad : any;
  private loginStatus = new BehaviorSubject<boolean>(false);
  private userDetails = new BehaviorSubject<User | null>(null);
  userDetails$ = this.userDetails.asObservable();
  loginStatus$ = this.loginStatus.asObservable();

  constructor(private http:HttpClient,private router : Router) {
    this.userPayLoad = this.decodedToken();
  }

  setLoginStatus(status: boolean) {
    this.loginStatus.next(status);
  }

    // login(username: string, password: string) : Observable<{ jwt: string,user: User }>{
    //   return this.http.post<{ jwt: string,user: User }>(this.baseApiUrl + '/api/users/login', { username, password })
    // }
  signUp(user : User) : Observable<User>{
    return this.http.post<User>(this.baseApiUrl + '/api/users/signup',user);
  }

  login(username: string, password: string) {
    this.http.post<{ jwt: string,user: User }>(this.baseApiUrl + '/api/users/login', { username, password }).subscribe((response) => {
        // Store the JWT in local storage
        localStorage.setItem('jwt', response.jwt);
        this.userPayLoad = this.decodedToken();
        this.userDetails.next(response.user);
        console.log(this.userDetails);
        console.log("Inside loginservice: " + response.jwt)
        this.setLoginStatus(true);
        //this.currentUser = response;
        this.router.navigate(['']);
    }, (error: HttpErrorResponse) => {
        if (error.status == 400) {
            alert("Invalid Login details");
        } else {
            console.log(error);
        }
    });
}

logout() {
  // Remove the JWT from local storage
  localStorage.removeItem('jwt');

  //this.currentUser = '';
  this.setLoginStatus(false);
  //this.router.navigate(['']);
}

  
  getJwtToken() {
    const token = localStorage.getItem('jwt');
    return token;
  }

  getUserDetails() {
    return this.getUserDetails;
  }

  isLogin() : Boolean {
    return !!localStorage.getItem('jwt');
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getJwtToken()!;
    return jwtHelper.decodeToken(token);
  }

  getUserId() {
    if(this.userPayLoad)
      return this.userPayLoad.id;
  }
  getFullNameFromToken() {
    if(this.userPayLoad)
      return this.userPayLoad.fullName;
  }

  getIsAdminFromToken() {
    if(this.userPayLoad)
      return this.userPayLoad.isAdmin;
  }
  // isLogin() : Observable<boolean> {
  //   const token = localStorage.getItem('jwt');
  //   if(token) {
  //     return of(true);
  //   }
  //   else {
  //     return of(false);
  //   }
  // }
  
  // constructor(private http: HttpClient) {
  //   this.isLoggedIn.pipe(
  //     switchMap(isLoggedIn => {
  //       if (isLoggedIn) {
  //         return this.http.get(this.baseApiUrl + '/api/user/profile');
  //       } else {
  //         return [];
  //       }
  //     })
  //   ).subscribe(profile => {
  //     // update user profile
  //   });
  // }

  // login(username: string, password: string) {
  //   this.http.post(this.baseApiUrl + '/api/users/login', { username, password }).subscribe(response => {
  //       console.log("ABD");
  //       console.log(response);
  //       this.loggedIn.next(true);
  //       this.currentUser = response;
  //       console.log(this.isLoggedIn)
  //       console.log(this.currentUser)
  //       this.router.navigate(['']);
  //   },(error:HttpErrorResponse) => {
  //     if(error.status == 400) {
  //       alert("Invalid Login details");
  //     }  
  //     else {
  //       console.log(error);
  //     }
  //   }
  //   );
  // }

  // logout() {
  //   // logout logic here
  //   console.log("LOGOUT SUCCESS")
  //     this.currentUser = ''
  //     this.loggedIn.next(false);
  //     this.router.navigate(['']);
  // }
 
}
