import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { HomeDashboardComponent } from './components/home-dashboard/home-dashboard.component';
import { MyordersComponent } from './components/myorders/myorders.component';
import { ViewcartComponent } from './components/viewcart/viewcart.component';
import { ProductformComponent } from './components/productform/productform.component';
import { AuthGuard } from './authguards/auth.guard';
import { AdminGuard } from './authguards/admin.guard';
import { ProductdetailsComponent } from './components/productdetails/productdetails.component';
import { EditformComponent } from './components/editform/editform.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

const routes: Routes = [
  {path : '',component:HomeDashboardComponent},
  {path : 'login',component:LoginComponent},
  {path : 'signup',component:SignupComponent},
  {path : 'myorders',component:MyordersComponent,canActivate:[AuthGuard]},
  {path : 'viewcart', component:ViewcartComponent,canActivate:[AuthGuard]},
  {path : 'addproduct', component:ProductformComponent,canActivate:[AdminGuard]},
  {path : 'productdetails/:id', component:ProductdetailsComponent},
  {path : 'editProduct/:id', component : EditformComponent,canActivate:[AdminGuard]},
  {path : '**', component:PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
