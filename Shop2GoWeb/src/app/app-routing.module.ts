import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { FavoritedProductsComponent } from './product/favorited-products/favorited-products.component';
import { GetProductsCategoryNameComponent } from './product/get-products-category-name/get-products-category-name.component';
import { OwnedPassiveProductsComponent } from './product/owned-passive-products/owned-passive-products.component';
import { OwnedProductsComponent } from './product/owned-products/owned-products.component';
import { ProductDetailsComponent } from './product/product-details/product-details.component';
import { ProductComponent } from './product/product.component';
import { SoldProductsComponent } from './product/sold-products/sold-products.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { AuthGuard } from './shared/auth/auth.guard';
import { ChatComponent } from './user/chat/chat.component';
import { HelpTicketComponent } from './user/help-ticket/help-ticket.component';
import { MyTicketsComponent } from './user/my-tickets/my-tickets.component';
import { PersonalInformationComponent } from './user/personal-information/personal-information.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { ReviewRatingComponent } from './user/review-rating/review-rating.component';
import { UserComponent } from './user/user.component';
import { VisitedUserProfileComponent } from './user/visited-user-profile/visited-user-profile.component';

const routes: Routes = [
  
  { path:'', redirectTo: '/home', pathMatch:'full'},// http://localhost:4200/Login

  { path:'Login',component: LoginComponent }, // http://localhost:4200/Login

  {path:'ForgotPassword',component:ForgotPasswordComponent}, // http://localhost:4200/ForgotPassword
  
  {path:'ResetPassword',component:ResetPasswordComponent}, // http://localhost:4200/ResetPassword

  { path:'home', component: HomeComponent },// http://localhost:4200/home

  { path:'User', component:UserComponent, canActivate:[AuthGuard], // http://localhost:4200/User
    children: [
      {path :'Personal-Information', component:PersonalInformationComponent}, // http://localhost:4200/User/Personal-Information
      {path:'Ticket',component:HelpTicketComponent}, // http://localhost:4200/User/Ticket
      {path:'My-Tickets',component:MyTicketsComponent}, // http://localhost:4200/User/My-Tickets

    ]
  },

  { path:'AdminPanel',component:AdminPanelComponent, canActivate:[AuthGuard], data:{permittedRoles:['Admin']} }, // http://localhost:4200/AdminPanel
  
  { path:'forbidden',component:ForbiddenComponent }, // http://localhost:4200/forbidden
  
  { path:'Product', component:ProductComponent, canActivate:[AuthGuard],data:{permittedRoles:['User']}, // http://localhost:4200/Product
      children:[
        {path:'Favorites',component:FavoritedProductsComponent}, // http://localhost:4200/Product/Favorites
        {path:'Active-Products',component:OwnedProductsComponent}, // http://localhost:4200/Product/Active-Products
        {path:'Inactive-Products',component:OwnedPassiveProductsComponent}, // http://localhost:4200/Product/Passive-Products
        {path:'Sold-Products',component:SoldProductsComponent}, // http://localhost:4200/Product/Sold-Products
      ]
  },

  {path:'Product-Details/:id',component:ProductDetailsComponent, canActivate:[AuthGuard]},  // http://localhost:4200/Product-Details/{id}

  { path:'Messages', component: ChatComponent,canActivate:[AuthGuard] },// http://localhost:4200/Messages
  { path:'Messages/:id', component: ChatComponent,canActivate:[AuthGuard] }, // http://localhost:4200/Messages/{id}
  { path:'User-Profile/:id', component: VisitedUserProfileComponent,canActivate:[AuthGuard]},// http://localhost:4200/User-Profile/{id}

  { path:'Rating', component: ReviewRatingComponent,canActivate:[AuthGuard]},// http://localhost:4200/Rating

  { path:'Products-Category/:name', component: GetProductsCategoryNameComponent},// http://localhost:4200/User-Profile/{name}
  { path: 'Products-Category/:id', component: GetProductsCategoryNameComponent },// http://localhost:4200/User-Profile/{id}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
