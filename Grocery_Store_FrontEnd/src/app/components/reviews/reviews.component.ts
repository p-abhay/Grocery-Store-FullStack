import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Review } from 'src/app/models/review.model';
import { ProductService } from 'src/app/services/product.service';
import { UserAuthService } from 'src/app/services/user-auth.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit{
  reviews: Review[] = [];
  currentUser = ''
  newReview: Review = { id: 0, productId: '', review: '', author: this.currentUser, date: ''};
  productId = '';
  constructor(private userauthService : UserAuthService,private router : Router, private productService : ProductService,private route : ActivatedRoute) {}
  ngOnInit(): void {
      this.productId = this.route.snapshot.paramMap.get('id')!;
      this.currentUser = this.userauthService.decodedToken().Name;
      this.productService.getAllReviews(this.productId).subscribe((allReviews) => {
        this.reviews = allReviews;
      });
      //console.log(this.newReview);
  }

  addComment() {
    if(this.currentUser.length == 0) {
      alert("You must be LoggedIn to post a Review");
      this.router.navigate(['login']);
      return;
    }
    if(this.newReview.review.length == 0) {
      alert("Review cannot be empty!");
      return;
    }
    //this.reviews.push(this.newReview);
    this.newReview.productId = this.productId;
    this.newReview.author = this.currentUser;
    this.productService.addReview(this.newReview).subscribe((addedReview) => {
      console.log(addedReview);
    });
    console.log("from review:" + this.newReview.author);
    this.newReview = {id: 0, productId: '', review: '', author: this.currentUser, date: ''};
    this.sortComments();
    this.ngOnInit();
  }

  sortComments() {
    this.reviews.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
  }
}
