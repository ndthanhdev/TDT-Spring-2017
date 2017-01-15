import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { AppNavBarComponent } from './app-nav-bar/app-nav-bar.component';
import { AppTeamComponent } from './app-team/app-team.component';
import { TopContentComponent } from './top-content/top-content.component';

@NgModule({
  declarations: [
    AppComponent,
    AppNavBarComponent,
    AppTeamComponent,
    TopContentComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    NgbModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
