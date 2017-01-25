import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
  host: { '(window:scroll)': 'track($event)' }
})
export class NavBarComponent implements OnInit {
  public isShrink = false;
  public isCollapsed = true;

  constructor() { }

  track(number: any) {
    this.isShrink = document.body.scrollTop > 55;
  }

  ngOnInit() {
  }

}
