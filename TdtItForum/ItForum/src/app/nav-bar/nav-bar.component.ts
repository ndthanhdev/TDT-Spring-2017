import {Component, OnInit, OnDestroy, Input, HostListener, Inject} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../../services/user/user.service';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit, OnDestroy {

  isAuthorized: boolean;
  userName: string;
  isShrink = false;
  isCollapsed = true;

  constructor(private router: Router, private  service: UserService) {
    this.service.authorizationChanged$.subscribe(v => {
      this.updateAuthorization();
    });
  }

  ngOnInit() {
    this.updateAuthorization();
  }

  ngOnDestroy(): void {

  }

  @HostListener('window:scroll', [])
  track() {
    this.isShrink = document.body.scrollTop > 55;
  }

  updateAuthorization(): void {
    this.isAuthorized = this.service.getIsAuthorized();
    if (this.isAuthorized) {
      this.userName = this.service.getUsername();
    }
  }

  logout(): void {
    localStorage.clear();
    this.service.notifyAuthorizedChanged();
    this.router.navigate(['/home']);
  }

}
