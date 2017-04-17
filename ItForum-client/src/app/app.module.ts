import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MaterialModule, MdDialogModule} from '@angular/material';

import {Ng2PageScrollModule} from 'ng2-page-scroll';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {JwtHelper} from 'angular2-jwt';
import {NgxDatatableModule} from '@swimlane/ngx-datatable';

import {AppRoutingModule} from '../modules/app-routing/app-routing.module';
import {AuthHttpModule} from '../modules/auth-http/auth-http.module';

import {AlertService} from '../services/alert/alert.service';
import {ConstantValuesService} from '../services/constantValues/constant-values.service';
import {RequestService} from '../services/request/request.service';

import {AppComponent} from './app.component';
import {HomeComponent} from './home/home.component';
import {NavBarComponent} from './nav-bar/nav-bar.component';
import {TeamComponent} from './team/team.component';
import {FooterComponent} from './footer/footer.component';
import {RegisterComponent} from './register/register.component';
import {AlertComponent} from './alert/alert.component';
import {LoginComponent} from './login/login.component';
import {UserService} from '../services/user/user.service';
import {ProfileComponent} from './profile/profile.component';
import {CategoryComponent} from './category/category.component';
import {ManageUserComponent} from './manage-user/manage-user.component';
import {ManageCategoryComponent} from './manage-category/manage-category.component';
import {VerifyPostComponent} from './verify-post/verify-post.component';
import {AdminService} from "../services/admin/admin.service";
import {AuthRequestService} from "../services/authRequest/auth-request.service";


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavBarComponent,
    TeamComponent,
    FooterComponent,
    RegisterComponent,
    AlertComponent,
    LoginComponent,
    ProfileComponent,
    CategoryComponent,
    ManageUserComponent,
    ManageCategoryComponent,
    VerifyPostComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AuthHttpModule,
    HttpModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    BrowserAnimationsModule,
    MdDialogModule,
    Ng2PageScrollModule.forRoot(),
    NgxDatatableModule
  ],
  providers: [
    JwtHelper,
    AlertService,
    ConstantValuesService,
    RequestService,
    AuthRequestService,
    UserService,
    AdminService
  ],
  bootstrap: [AppComponent],
  entryComponents: [AlertComponent]
})
export class AppModule {
}
