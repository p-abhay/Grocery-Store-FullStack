import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cart } from 'src/app/models/cart.model';
import { Product } from 'src/app/models/product.model';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';
import { UserAuthService } from 'src/app/services/user-auth.service';

@Component({
  selector: 'app-productdetails',
  templateUrl: './productdetails.component.html',
  styleUrls: ['./productdetails.component.css']
})
export class ProductdetailsComponent implements OnInit {

  private productId : string = '';
  private userId : string = '';
  productDetails : Product = {
    id : '',
    name: 'Orange Juice',
    description: '',
    category: '',
    availableQuantity: 0,
    imageUrl: 'https://www.shutterstock.com/image-vector/orange-juice-drink-carton-mockup-600w-1202842027.jpg',
    price: 70,
    discount: 0,
    specification: ''
  };
  cartQty : number = 0;
  cart : Cart = {
    id : 0,
    userId : '',
    productId : '',
    quantity : 0
  }
  constructor(private route : ActivatedRoute,private productService : ProductService,private userauthService : UserAuthService,private router : Router,private cartService : CartService) {}

  ngOnInit() {
    this.productId = this.route.snapshot.paramMap.get('id')!;
    
    console.log("inside details");
    this.productService.getProductById(this.productId)
    .subscribe(
      (details) => {
        this.productDetails = details;
        //console.log("from ngonit product details "+this.productDetails)
      }
    )
    this.userId = this.userauthService.decodedToken().Id!;
  }

  decrease() {
    if(this.cartQty >= 1)
      this.cartQty -= 1;
  }
  increase() {
    if((this.cartQty < 10) && (this.cartQty < this.productDetails.availableQuantity)) {
      this.cartQty += 1;
    }
    else if(this.cartQty == this.productDetails.availableQuantity) {
      alert("Max Quantity reached");
    }
    else {
      alert("Cannot Add more than 10 quantities");
    }
  }

  addToCart() {
    //event.preventDefault();
    if(this.cartQty == 0) {
      alert("Quantity should be more than 0");
      return;
    }
    if(this.productDetails.availableQuantity == 0) {
      alert("Product is out of stock!");
      return;
    }
    if(this.userauthService.isLogin()) {
      this.cart.userId = this.userId;
      this.cart.productId = this.productId;
      this.cart.quantity = this.cartQty;
      this.cartService.addToCart(this.cart)
      .subscribe(res => {
        console.log("from service response " + res);
      });
      console.log("line 68"+this.cart);
      this.router.navigate(['viewcart'])
      alert("Added");    }
    else {
      alert("Login first");
      this.router.navigate(["login"])
    }
  }

}
