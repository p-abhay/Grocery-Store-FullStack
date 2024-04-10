import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserAuthService } from 'src/app/services/user-auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user : User = {
    id : '',
    fullName : '',
    email : '',
    phoneNumber : '',
    password : '',
    isAdmin : false
  };
  passwordValue = {
    dirty: false,
    errors: {
      minlength: false
    }
  };
  emailValue = {
    dirty : false,
    errors: {
      valid : false
    }
  };
  res : any;
  constructor(private router : Router,private userauthService : UserAuthService) {}
  ngOnInit(): void {
      
  }
  onLogin() {
    if (!this.user.email || !this.user.password) {
      alert('Email and password are required.');
      return;
    }
    if (!/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(this.user.email)) {
      alert('Email must be a valid email address.');
      return;
    }
    if (this.user.password.length < 8) {
      alert('Password must be at least 8 characters long.');
      return;
    }
    this.userauthService.login(this.user.email,this.user.password);
      
      console.log("inside login component ()")
      //this.router.navigate(['']);
    // this.router.navigate(['']);
  }

  onPasswordChange() {
    this.passwordValue.dirty = true;
    this.passwordValue.errors.minlength = this.user.password.length < 8;
  }
  
  onEmailChange() {
    this.emailValue.dirty = true;
    if (!/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(this.user.email)) {
      this.emailValue.errors.valid = true;
    }
  }
  
}
