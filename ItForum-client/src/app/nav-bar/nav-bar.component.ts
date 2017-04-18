import {Component, OnInit, OnDestroy, Input, HostListener, Inject} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../../services/user/user.service';
import {AlertService} from "../../services/alert/alert.service";


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit, OnDestroy {

  @Input()
  isAuthorized: boolean;

  @Input()
  userName: string;

  @Input()
  isVerify: boolean;

  @Input()
  isAdmin: boolean;

  @Input()
  isMod: boolean;

  isShrink = false;
  isCollapsed = true;

  constructor(private router: Router, private  service: UserService, private alert: AlertService) {

  }

  ngOnInit() {
  }

  ngOnDestroy(): void {

  }

  @HostListener('window:scroll', [])
  track() {
    this.isShrink = document.body.scrollTop > 55;
  }


  logout(): void {
    localStorage.clear();
    this.service.notifyAuthorizedChanged();
    this.router.navigate(['/home']);
    this.alert.openSnackbar("You logged out");
  }

}
