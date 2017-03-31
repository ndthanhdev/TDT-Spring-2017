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
  profile: ProfileModel;

  constructor(private  service: UserService) {
    this.service.authorizationChanged$.subscribe(v => {
      this.authorizationChanged();
    });
    this.isAuthorized = false;
  }

  ngOnInit() {
    this.authorizationChanged();
  }

  authorizationChanged(): void {
    this.isAuthorized = UserService.isAuthorized();
    if (this.isAuthorized) {
      this.userName = 'unknown';
    }
  }
}
