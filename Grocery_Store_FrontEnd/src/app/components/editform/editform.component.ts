import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-editform',
  templateUrl: './editform.component.html',
  styleUrls: ['./editform.component.css']
})
export class EditformComponent {
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
  productId : string = ''
  //edit : boolean = false;
  constructor(private productService : ProductService,private route : ActivatedRoute,private router : Router) {}

  ngOnInit(): void {
      this.productId = this.route.snapshot.paramMap.get('id')!;
      this.productService.getProductById(this.productId).subscribe(
        product => {
          this.product = product;
        }
      )
      
  }

 
  updateProduct() {
    this.productService.updateProduct(this.product).subscribe(
      res => {
        console.log(res);
      }
    )
    this.router.navigate(['']);
  }
}
