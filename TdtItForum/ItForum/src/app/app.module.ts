import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {MaterialModule, MdDialogModule} from '@angular/material';

import {Ng2PageScrollModule} from 'ng2-page-scroll';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import {AppRoutingModule} from './app-routing/app-routing.module';

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
import {RegisterService} from '../services/register/register.service';
import {LoginComponent} from './login/login.component';
import {UserService} from "../services/user/user.service";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavBarComponent,
    TeamComponent,
    FooterComponent,
    RegisterComponent,
    AlertComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    MaterialModule,
    Ng2PageScrollModule.forRoot(),
  ],
  providers: [
    AlertService,
    ConstantValuesService,
    RequestService,
    RegisterService,
    UserService],
  bootstrap: [AppComponent],
  entryComponents: [AlertComponent]
})
export class AppModule {
}
