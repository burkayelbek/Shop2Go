import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductFormComponent } from './product/product-form/product-form.component';
import { MatInputModule } from '@angular/material/input';
import { NavbarComponent } from './navbar/navbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { ToastrModule } from 'ngx-toastr';
import { RegistrationComponent } from './user/registration/registration.component';
import { MatDialogModule } from '@angular/material/dialog';
import { LoginComponent } from './login/login.component';
import { MatTabsModule } from '@angular/material/tabs';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './user/user.component';
import { PersonalInformationComponent } from './user/personal-information/personal-information.component';
import { interceptorProviders } from './shared/interceptor/interceptor-const';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { ProductComponent } from './product/product.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { OwnedProductsComponent } from './product/owned-products/owned-products.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FavoritedProductsComponent } from './product/favorited-products/favorited-products.component';
import { EditPersonalSettingsDialogComponent } from './user/dialogs/edit-personal-settings-dialog/edit-personal-settings-dialog.component';
import { ChangePasswordDialogComponent } from './user/dialogs/change-password-dialog/change-password-dialog.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { HelpTicketComponent } from './user/help-ticket/help-ticket.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AdminHelpTicketComponent } from './admin/admin-help-ticket/admin-help-ticket.component';
import { MatSortModule } from '@angular/material/sort';
import { MyTicketsComponent } from './user/my-tickets/my-tickets.component';
import { AdminHelpTicketDetailsDialogComponent } from './admin/dialogs/admin-help-ticket-details-dialog/admin-help-ticket-details-dialog.component';
import { ProductDetailsComponent } from './product/product-details/product-details.component';
import { ProductManagementComponent } from './admin/product-management/product-management.component';
import { ApproveProductDialogComponent } from './admin/dialogs/approve-product-dialog/approve-product-dialog.component';
import { RejectProductDialogComponent } from './admin/dialogs/reject-product-dialog/reject-product-dialog.component';
import { OwnedPassiveProductsComponent } from './product/owned-passive-products/owned-passive-products.component';
import { SafePipe } from './shared/safe.pipe';
import { NgImageSliderModule } from 'ng-image-slider';
import { HelpTicketDetailsDialogComponent } from './user/dialogs/help-ticket-details-dialog/help-ticket-details-dialog.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ApproveTicketCloseDialogComponent } from './user/dialogs/approve-ticket-close-dialog/approve-ticket-close-dialog.component';
import { ChatComponent } from './user/chat/chat.component';
import { ChatDialogComponent } from './user/dialogs/chat-dialog/chat-dialog.component';
import { ProductDeleteConfirmationDialogComponent } from './product/dialogs/product-delete-confirmation-dialog/product-delete-confirmation-dialog.component';
import { EditProductDialogComponent } from './product/dialogs/edit-product-dialog/edit-product-dialog.component';
import { VisitedUserProfileComponent } from './user/visited-user-profile/visited-user-profile.component';
import { ReviewRatingComponent } from './user/review-rating/review-rating.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ProductChangeStatusDialogComponent } from './product/dialogs/product-change-status-dialog/product-change-status-dialog.component';
import { SoldProductsComponent } from './product/sold-products/sold-products.component';
import { MatStepperModule } from '@angular/material/stepper';
import { AddCategoryDialogComponent } from './admin/dialogs/add-category-dialog/add-category-dialog.component';
import { UserListComponent } from './admin/user-list/user-list.component';
import { UserListDialogComponent } from './admin/dialogs/user-list-dialog/user-list-dialog.component';
import { GetProductsCategoryNameComponent } from './product/get-products-category-name/get-products-category-name.component';
import { CategoryOperationsDialogComponent } from './admin/dialogs/category-operations-dialog/category-operations-dialog.component';
import { EditCategoryDialogComponent } from './admin/dialogs/edit-category-dialog/edit-category-dialog.component';
import { DeleteCategoryConfirmationDialogComponent } from './admin/dialogs/delete-category-confirmation-dialog/delete-category-confirmation-dialog.component';
import { RestrictUserDialogComponent } from './admin/dialogs/restrict-user-dialog/restrict-user-dialog.component';
import { RemoveRestrictionDialogComponent } from './admin/dialogs/remove-restriction-dialog/remove-restriction-dialog.component';
import { SendMailDialogComponent } from './admin/dialogs/send-mail-dialog/send-mail-dialog.component';
import { UserProductRejectedMessageDialogComponent } from './product/dialogs/user-product-rejected-message-dialog/user-product-rejected-message-dialog.component';
import { DeleteReviewConfirmationDialogComponent } from './admin/dialogs/delete-review-confirmation-dialog/delete-review-confirmation-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductFormComponent,
    NavbarComponent,
    RegistrationComponent,
    LoginComponent,
    HomeComponent,
    UserComponent,
    PersonalInformationComponent,
    AdminPanelComponent,
    ForbiddenComponent,
    ProductComponent,
    ProductFormComponent,
    OwnedProductsComponent,
    FavoritedProductsComponent,
    EditPersonalSettingsDialogComponent,
    ChangePasswordDialogComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    HelpTicketComponent,
    AdminHelpTicketComponent,
    MyTicketsComponent,
    AdminHelpTicketDetailsDialogComponent,
    ProductDetailsComponent,
    ProductManagementComponent,
    ApproveProductDialogComponent,
    RejectProductDialogComponent,
    OwnedPassiveProductsComponent,
    SafePipe,
    HelpTicketDetailsDialogComponent,
    ApproveTicketCloseDialogComponent,
    ChatComponent,
    ChatDialogComponent,
    ProductDeleteConfirmationDialogComponent,
    EditProductDialogComponent,
    VisitedUserProfileComponent,
    ReviewRatingComponent,
    ProductChangeStatusDialogComponent,
    SoldProductsComponent,
    AddCategoryDialogComponent,
    UserListComponent,
    UserListDialogComponent,
    GetProductsCategoryNameComponent,
    CategoryOperationsDialogComponent,
    EditCategoryDialogComponent,
    DeleteCategoryConfirmationDialogComponent,
    RestrictUserDialogComponent,
    RemoveRestrictionDialogComponent,
    SendMailDialogComponent,
    UserProductRejectedMessageDialogComponent,
    DeleteReviewConfirmationDialogComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    FlexLayoutModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatToolbarModule,
    MatMenuModule,
    MatDividerModule,
    ToastrModule.forRoot({
      timeOut: 1500,
      progressBar: true,
      maxOpened: 4,
      preventDuplicates: true,
      countDuplicates: true,
      autoDismiss: true,
      positionClass: 'toast-bottom-right',
    }),
    MatDialogModule,
    MatTabsModule,
    MatSelectModule,
    MatExpansionModule,
    FontAwesomeModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    NgImageSliderModule,
    MatTooltipModule,
    MatCheckboxModule,
    MatSnackBarModule,
    MatStepperModule
  ],
 
  providers: [interceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
