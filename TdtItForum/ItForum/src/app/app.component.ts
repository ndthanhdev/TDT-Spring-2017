import {Component, OnInit} from '@angular/core';
import {JwtHelper} from 'angular2-jwt';
import {ConstantValuesService} from '../services/constantValues/constant-values.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {


  constructor() {
  }

  ngOnInit() {
  }

}
