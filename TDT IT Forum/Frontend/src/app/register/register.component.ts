import {Component, OnInit} from '@angular/core';
import {RegisterInformation} from '../models/registerInformation';

@Component({
  moduleId: module.id,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  admissionYears: number[] = [];
  model: RegisterInformation;

  constructor() {
  }

  ngOnInit() {
    let currentYear = (new Date()).getFullYear();
    for (let _i = currentYear; _i >= 1997; _i--) {
      this.admissionYears.push(_i);
    }
    this.model = new RegisterInformation('', '', '', currentYear, '', '');
  }

  submit():void {
    console.log(this.model.admissionYear);
  }

}
