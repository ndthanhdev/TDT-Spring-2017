import {Injectable} from '@angular/core';
import {AlertComponent} from '../../app/alert/alert.component';
import {MdDialog, MdSnackBar} from '@angular/material';

@Injectable()
export class AlertService {

  constructor(public dialog: MdDialog, private snackBar: MdSnackBar) {
  }

  openDialog(message: string): void {
    const dialogRef = this.dialog.open(AlertComponent);
    dialogRef.componentInstance.message = message;
  }

  openReadErrorInLog(err: any): void {
    console.log(err);
    this.openDialog('Oops, There\'s some error occurred.');
  }

  openSnackbar(message: string, duration: number = 2000) {
    this.snackBar.open("You logged", null, {duration: duration});
  }
}
