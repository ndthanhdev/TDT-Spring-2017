import {Component, OnInit} from '@angular/core';
import {JwtHelper} from 'angular2-jwt';
import {ConstantValuesService} from '../services/constantValues/constant-values.service';
import {UserService} from '../services/user/user.service';
import {ProfileModel} from '../models/profile.model';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  isAuthorized: boolean;
  userName: string;
  profile = new ProfileModel('', '123', '', '', 1997, '', '', false, []);


  constructor(private  service: UserService) {
    this.service.authorizationChanged$.subscribe(v => {
      this.authorizationChanged();
    });
    this.isAuthorized = false;
  }

  ngOnInit() {
    this.authorizationChanged();

  }

  async authorizationChanged(): Promise<void> {
    this.isAuthorized = UserService.isAuthorized();
    if (this.isAuthorized) {
      this.userName = 'unknown';
      const lol = this.service.getJwt();
      const userId = this.service.getJwt()[ConstantValuesService.JWT_USERNAME];
      const payload = await this.service.getProfile(userId);
      if (payload.statusCode === 0) {
        this.profile = payload.data;
      }
    }
    else {
      this.profile = null;
    }
  }
}
