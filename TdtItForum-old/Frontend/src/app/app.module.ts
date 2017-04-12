// Angular Module
import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {MaterialModule } from  '@angular/material'

// 3rd party Module
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {Ng2PageScrollModule} from 'ng2-page-scroll';

import {AppRoutingModule} from './app-routing/app-routing.module';
import {AuthenticationService} from '../services/authentication.service';
import {RegisterService} from '../services/register.service';

import {AppComponent} from './app.component';
import {TopContentComponent} from './top-content/top-content.component';
import {TeamComponent} from './team/team.component';
import {NavBarComponent} from './nav-bar/nav-bar.component';
import {FooterComponent} from './footer/footer.component';
import {QuickViewComponent} from './quick-view/quick-view.component';
import {LoginComponent} from './login/login.component';
import {HomeComponent} from './home/home.component';
import {RegisterComponent} from './register/register.component';
import { AlertComponent } from './alert/alert.component';

@NgModule({
  declarations: [
    AppComponent,
    TopContentComponent,
    TeamComponent,
    NavBarComponent,
    FooterComponent,
    QuickViewComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MaterialModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    Ng2PageScrollModule.forRoot()
  ],
  providers: [AuthenticationService, RegisterService],
  bootstrap: [AppComponent],
  entryComponents: [AlertComponent]
})
export class AppModule {
}
