import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { HomeDashboardComponent } from './components/home-dashboard/home-dashboard.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MyordersComponent } from './components/myorders/myorders.component';
import { ViewcartComponent } from './components/viewcart/viewcart.component';
import { ProductformComponent } from './components/productform/productform.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { ProductdetailsComponent } from './components/productdetails/productdetails.component';
import { EditformComponent } from './components/editform/editform.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ReviewsComponent } from './components/reviews/reviews.component';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    SignupComponent,
    HomeDashboardComponent,
    MyordersComponent,
    ViewcartComponent,
    ProductformComponent,
    ProductdetailsComponent,
    EditformComponent,
    PageNotFoundComponent,
    ReviewsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    NgxPaginationModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
