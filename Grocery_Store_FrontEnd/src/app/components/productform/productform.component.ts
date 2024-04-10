import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-productform',
  templateUrl: './productform.component.html',
  styleUrls: ['./productform.component.css']
})
export class ProductformComponent implements OnInit{

  product : Product = {
    id : '',
    name: '',
    description: '',
    category: '',
    availableQuantity: 0,
    imageUrl: '',
    price: 0,
    discount: 0,
    specification: ''
  }
  
  constructor(private productService : ProductService,private route : ActivatedRoute,private router : Router) {}

  ngOnInit(): void {
      
  }

  addProduct() {
    
    console.log("product from form")
    console.log(this.product);
    this.product.id = "00000000-0000-0000-0000-000000000000"
    this.productService.addProduct(this.product)
    .subscribe({
      next : (product) => {
        console.log(product);
        alert("Product added");
        this.router.navigate(['']);
      }
    })
  }

}
