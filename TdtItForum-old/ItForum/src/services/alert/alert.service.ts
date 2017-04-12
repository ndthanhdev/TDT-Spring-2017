import {Injectable} from '@angular/core';
import {AlertComponent} from '../../app/alert/alert.component';
import {MdDialog} from '@angular/material';

@Injectable()
export class AlertService {

  constructor(public dialog: MdDialog) {
  }

  openDialog(message: string): void {
    const dialogRef = this.dialog.open(AlertComponent);
    dialogRef.componentInstance.message = message;
  }

  openReadErrorInLog(err: any): void {
    console.log(err);
    this.openDialog('Oops, There\'s some error occurred.');
  }
}
