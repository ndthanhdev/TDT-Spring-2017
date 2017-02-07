import { Component, EventEmitter, OnInit, Input, Output } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @Output() triedLoginEventHandler = new EventEmitter<void>();

  constructor() { }

  ngOnInit() {
  }

  login(): void {
    this.triedLoginEventHandler.emit();
  }

}
