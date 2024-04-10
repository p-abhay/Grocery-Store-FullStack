import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserAuthService } from 'src/app/services/user-auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  userName: string = '';
  isAdmin: boolean = false;
  isAdmin2: boolean = false;
  token: any;
  constructor(
    private userauthService: UserAuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userauthService.loginStatus$.subscribe((status) => {
      console.log('value from service navbar ' + status);
      this.isLoggedIn = status;
    });
    // this.userauthService.isLogin().subscribe(status => {
    //   this.isLoggedIn = status;
    // })
    this.userauthService.userDetails$.subscribe((user) => {
      if (user != null) {
        this.userName = user.fullName;
        this.isAdmin = user.isAdmin;
        console.log('type of isadmin from nav ' + typeof user?.isAdmin);
        this.ngOnInit();
      }
    });
    console.log('inside onint navbar ' + this.isLoggedIn);
    this.token = localStorage.getItem('jwt');
    if (this.token) {
      this.userauthService.userDetails$.subscribe((user) => {
        this.userName = user?.fullName || this.token.fullName;
        this.isAdmin2 = JSON.parse(this.token.isAdmin);
      });
    }
    this.userName = this.userauthService.decodedToken().Name;
    // this.userName = this.userauthService.getFullNameFromToken();
    //this.isAdmin = this.userauthService.getIsAdminFromToken();
    console.log('isadmin: navbar' + this.isAdmin);
    this.isAdmin2 = JSON.parse(this.userauthService.decodedToken().isAdmin);
    console.log('isAdmin2 navbar' + this.isAdmin2);
    // console.log("token :"+this.token)
  }

  myOrders(event: Event) {
    event.preventDefault();
    console.log('Inside myorders');
    //console.log(this.currentUser);
    this.router.navigate(['myorders']);
  }
  viewCart(event: Event) {
    event.preventDefault();
    //console.log("inside view cart "+this.isLoggedIn);
    this.router.navigate(['viewcart']);
  }

  addProduct(event: Event) {
    event.preventDefault();
    this.router.navigate(['addproduct']);
  }

  logOut() {
    this.userauthService.logout();
    this.router.navigate(['']);
  }
}
