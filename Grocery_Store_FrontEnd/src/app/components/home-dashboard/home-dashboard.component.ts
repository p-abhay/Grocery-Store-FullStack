import { Component, OnInit } from '@angular/core';
import {Product} from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { UserAuthService } from 'src/app/services/user-auth.service';


@Component({
  selector: 'app-home-dashboard',
  templateUrl: './home-dashboard.component.html',
  styleUrls: ['./home-dashboard.component.css']
})
export class HomeDashboardComponent implements OnInit{

  products : Product[] = [];
  isAdmin? : boolean = false;
  isAdmin2 : boolean = false;
  testBool = false;
  filteredProducts : Product[] = []
  searchText : string = ''
  selectedCategory : string = ''
  categories : string[] = []
  page : number = 1;
  selectedSortType : string = '';
  allSortingTypes : string[] = [
    "Price--Low to High",
    "Price--High to Low"
  ];
  constructor(private productService : ProductService,private userauthService : UserAuthService) {}

  ngOnInit(): void {
      //debugger;
      this.productService.getAllProducts()
      .subscribe({
        next: (products) => {
          console.log(products);
          this.products = products;
          this.filteredProducts = products;
          this.categories =  [...new Set(products.map(product => product.category))];
        },
        error: (response) => {
          console.log(response);
        }
      });
      this.userauthService.userDetails$.subscribe(user => {
        this.isAdmin = user?.isAdmin
      })
      this.isAdmin2 = JSON.parse(this.userauthService.decodedToken().isAdmin);
      //console.log(this.isAdmin2);
      console.log(typeof(this.isAdmin))
      console.log("isAdmin : " + this.isAdmin + " isAdmin2: " + this.isAdmin2)
      
      
  }

  editProduct(product : Product) {
    console.log("edit");
    this.ngOnInit();
    
  }

  deleteProduct(product : Product) {
    console.log(product);
    this.productService.deleteProduct(product).subscribe(res => {
      console.log(res);
      this.ngOnInit();
    })
    alert("Product Deleted");
  }
  
  updateProducts() {
    this.filteredProducts = this.getFilteredProducts();
    let matchesSort = true;
    if(this.selectedSortType == "Price--Low to High") {
      this.filteredProducts.sort((a,b) =>
      a.price - b.price);
    }
    if(this.selectedSortType == "Price--High to Low") {
      this.filteredProducts.sort((a,b) =>
      b.price - a.price);
    }
  }

  getFilteredProducts() {
    return this.products.filter(product => {
      let matchesSearchText = true;
      let matchesCategory = true;
      
      if(this.searchText) {
        const searchTextLower = this.searchText.toLowerCase();
        matchesSearchText = product.name.toLowerCase().includes(searchTextLower) || product.description.toLowerCase().includes(searchTextLower);
      }
      if(this.selectedCategory) {
        matchesCategory = product.category === this.selectedCategory;
      }
      
      return matchesCategory && matchesSearchText;
    });
  }
}
