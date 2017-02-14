import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
  host: { '(window:scroll)': 'track($event)' }
})
export class NavBarComponent implements OnInit {

  @Input() isAuthorized: boolean;

  public isShrink = false;
  public isCollapsed = true;

  constructor() { }

  track(number: any) {
    this.isShrink = document.body.scrollTop > 55;
  }
  ngOnInit() {
  }

}
