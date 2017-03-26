import {Component, OnInit} from '@angular/core';
import {MdDialog} from '@angular/material';

import {AlertComponent} from '../alert/alert.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(public dialog: MdDialog) {
  }

  openDialog() {
    let dialogRef = this.dialog.open(AlertComponent);
    dialogRef.componentInstance.message = 'hello u';
  }

  ngOnInit() {
  }

}
