import {Component, HostListener, OnInit} from '@angular/core';
import {JwtHelper} from 'angular2-jwt';
import {ConstantValuesService, RegisteredRoles} from '../services/constantValues/constant-values.service';
import {UserService} from '../services/user/user.service';
import {User} from '../models/user.model';
import {Router} from "@angular/router";
import {AlertService} from "../services/alert/alert.service";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  isAuthorized: boolean;
  profile: User;

  isAdmin: boolean;
  isMod: boolean;

  isShrink = false;
  isCollapsed = true;

  constructor(private router: Router, private  service: UserService, private alert: AlertService) {
    this.service.authorizationChanged$.subscribe(v => {
      this.authorizationChanged();
    });
    this.isAuthorized = false;
  }

  ngOnInit() {
    this.profile = new User('', '', '', '', 1997, '', '', false, [], []);
    this.authorizationChanged();

  }

  async authorizationChanged(): Promise<void> {
    this.isAuthorized = UserService.isAuthorized();
    if (this.isAuthorized) {
      const jwt = this.service.getJwt();
      const userId = jwt[ConstantValuesService.JWT_USERNAME];
      this.isAdmin = jwt[ConstantValuesService.JWT_ROLE].indexOf(RegisteredRoles.Adminstrator) !== -1;
      this.isMod = jwt[ConstantValuesService.JWT_ROLE].indexOf(RegisteredRoles.Moderator) !== -1;
      const payload = await this.service.getProfile(userId);
      if (payload.statusCode === 0) {
        try {


          this.profile = payload.data;
        }
        catch (err){

        }
      }
    }
    else {
      this.profile = null;
      this.isAdmin = false;
      this.isMod = false;
    }
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
