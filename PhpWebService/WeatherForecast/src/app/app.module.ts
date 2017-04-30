import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Http, HttpModule} from '@angular/http';

import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {MdButtonModule, MdInputModule, MdTabsModule} from "@angular/material";

import {ChartModule} from 'primeng/primeng';
import {NgxDatatableModule} from "@swimlane/ngx-datatable";

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        BrowserAnimationsModule,
        MdTabsModule,
        ChartModule,
        NgxDatatableModule,
        MdInputModule,
        MdButtonModule

    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
