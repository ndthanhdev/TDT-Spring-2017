import {Component, OnInit, Input, HostListener, Inject} from '@angular/core';
import {DOCUMENT} from '@angular/platform-browser';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
  // host: { '(window:scroll)': 'track($event)' }
})
export class NavBarComponent implements OnInit {

  @Input()
  isAuthrized: boolean;

  @Input()
  userName: string;

  public isShrink = false;
  public isCollapsed = true;

  constructor(@Inject(DOCUMENT) private document: Document) {
  }

  @HostListener('window:scroll', [])
  track() {
    console.log(this.document.body.scrollTop);
  }

  ngOnInit() {
  }
}
