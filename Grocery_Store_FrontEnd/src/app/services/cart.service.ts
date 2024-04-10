import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Product } from '../models/product.model';
import { Cart } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private baseApiUrl = environment.baseApiURL
  constructor(private http : HttpClient) { }

  viewCart(id : any) : Observable<any[]>{
    var userId = id
    const body: Cart = { 
      id : 0,
      userId : id,
      productId : '00000000-0000-0000-0000-000000000000',
      quantity : 0
    }
    return this.http.post<any[]>(this.baseApiUrl + '/viewcart',body);
    // const headers = new HttpHeaders().set('Content-Type', 'text/plain');
    // return this.http.post<any[]>(this.baseApiUrl + '/viewcart', id, { headers });
  }

  addToCart(cart : Cart) : Observable<Cart>{
    console.log("INside cartService" + cart + "end")
    //const body = {userId,productId};
    const body : Cart = {id : cart.id,userId : cart.userId,productId : cart.productId,quantity : cart.quantity}
    console.log("body" + body +"end")
    return this.http.post<Cart>(this.baseApiUrl + '/cart/add',cart);
  }

  removeCart(cart : Cart) : Observable<Cart> {
    return this.http.delete<Cart>(this.baseApiUrl + '/cart/delete',{body:cart});
  }

  placeOrder(userId : string,product : Product[]) : Observable<string>{
    return this.http.post<string>(this.baseApiUrl + '/cart/placeOrder',{userId,product});
  }

  myOrders(userId : string) : Observable<any[]>{
    return this.http.get<any[]>(`${this.baseApiUrl}/api/cart/?userId=${userId}`);
  }
}
