import {Component, OnInit} from '@angular/core';
import {MdDialogRef} from "@angular/material";

@Component({
  selector: 'app-reply',
  templateUrl: './reply.component.html',
  styleUrls: ['./reply.component.css']
})
export class ReplyComponent implements OnInit {

  content: String;

  constructor(public dialogRef: MdDialogRef<ReplyComponent>) {
  }

  ngOnInit() {
  }

  ok(): void {
    this.dialogRef.close(this.content);
  }

}
