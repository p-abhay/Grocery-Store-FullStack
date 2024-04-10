import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserAuthService } from '../services/user-auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private userauthService : UserAuthService,private router : Router){}

  canActivate() : boolean{
    if(this.userauthService.isLogin() && JSON.parse(this.userauthService.decodedToken().isAdmin)) {
      return true;
    }
    else {
      alert("You are not admin");
      this.router.navigate(['']);
      return false;
    }
  }
  
}
