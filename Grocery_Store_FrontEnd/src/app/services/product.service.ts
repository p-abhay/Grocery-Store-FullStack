import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Product } from '../models/product.model';
import { Observable } from 'rxjs';
import { Review } from '../models/review.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseApiUrl : string = environment.baseApiURL;
  constructor(private http : HttpClient) { }

  getAllProducts():Observable<Product[]> {
    return this.http.get<Product[]>(this.baseApiUrl + '/getAllProducts');
  }

  getProductById(id : string) : Observable<Product>{
    const body = {id}
    return this.http.post<Product>(this.baseApiUrl + '/getById',body);
  }
  addProduct(product : Product) : Observable<Product>{
    return this.http.post<Product>(this.baseApiUrl + '/addProduct',product);
  }

  deleteProduct(product : Product) : Observable<Product>{
    return this.http.delete<Product>(this.baseApiUrl + '/api/product/delete',{body:product});
  }

  updateProduct(product : Product) : Observable<Product> {
    return this.http.put<Product>(this.baseApiUrl + '/api/product/update',product);
  }

  addReview(review : Review) : Observable<Review>{
    return this.http.post<Review>(this.baseApiUrl + '/api/product/reviews/add',review);
  }

  getAllReviews(productId : string) : Observable<Review[]> {
    return this.http.get<Review[]>(`${this.baseApiUrl}/api/product/reviews/?productId=${productId}`);
  }
}
