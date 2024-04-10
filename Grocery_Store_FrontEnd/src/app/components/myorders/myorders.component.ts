import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { UserAuthService } from 'src/app/services/user-auth.service';

@Component({
  selector: 'app-myorders',
  templateUrl: './myorders.component.html',
  styleUrls: ['./myorders.component.css']
})
export class MyordersComponent implements OnInit {

  product : any = {
    image: 'https://www.coca-colacanada.ca/content/dam/nagbrands/ca/coke/en/specialtysoda/coca-cola-de-mexico/Coca-ColadeMexico355mLBottle-productImageSmall.png',
    name : 'Coco Cola'
  }
  orders : any[] = [
    {
      id : 'hdfh3436434',
      date : "19/06/2023",
      quantity : 23,
      product : this.product
    }
  ]
  userId : string = ''
  constructor(private cartService : CartService,private userauthService : UserAuthService) {}

  ngOnInit(): void {
      this.userId = this.userauthService.decodedToken().Id;
      this.cartService.myOrders(this.userId).subscribe(orders => {
        this.orders = orders;
        console.log(this.orders);
      })
      this.orders.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
      
  }
}

