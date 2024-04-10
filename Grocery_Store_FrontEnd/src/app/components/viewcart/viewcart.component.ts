import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cart } from 'src/app/models/cart.model';
import { Product } from 'src/app/models/product.model';
import { User } from 'src/app/models/user.model';
import { CartService } from 'src/app/services/cart.service';
import { UserAuthService } from 'src/app/services/user-auth.service';

@Component({
  selector: 'app-viewcart',
  templateUrl: './viewcart.component.html',
  styleUrls: ['./viewcart.component.css']
})
export class ViewcartComponent implements OnInit{

  cartProducts : any[] = [];
  user : User = {
    id : '',
    fullName : '',
    email : '',
    phoneNumber : '',
    password : '',
    isAdmin : false
  }
  userId : string = ''
  constructor(private cartService : CartService,private userauthService : UserAuthService,private router : Router) {}

  ngOnInit(): void {
    console.log(this.userauthService.decodedToken());
    //console.log(this.userauthService.getUserId());
      var user = this.userauthService.decodedToken();
      this.user.id = user.Id;
      this.user.fullName = user.Name;
      this.user.email = user.Email;
      this.user.phoneNumber = user.PhoneNumber;
      this.user.isAdmin = user.isAdmin;
    
      console.log("user : "+ this.user.id);
      this.viewCart();
  }
  removeFromCart(product : Product) {
    var cart : Cart = {
      id : 0,
      userId : this.user.id,
      productId : product.id,
      quantity : product.availableQuantity
    }
    this.cartService.removeCart(cart)
    .subscribe(res => {
      console.log(res);
      this.ngOnInit();
      //this.router.navigate(['viewcart']);
    })
  }

  viewCart() {
    this.cartService.viewCart(this.user.id)
    .subscribe(
      (products : any[]) => {
        this.cartProducts = products;
        console.log(this.cartProducts);
      }
    )
  }

  placeOrder() {
    console.log("inside place")
    if(this.cartProducts.length == 0) {
      alert("No item in cart")
      return;
    }
    
    this.cartService.placeOrder(this.user.id,this.cartProducts)
    .subscribe(
      res => {
        console.log(res);
        
      }
    )
    alert("Order placed");
    
    for(var i = 0; i < this.cartProducts.length; i++) {
      this.removeFromCart(this.cartProducts[i]);
    }
    this.cartProducts = [];
  }
}
