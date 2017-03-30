import {Component, OnInit} from '@angular/core';
import {UserService} from '../../services/user/user.service';
import {LoginModel} from '../../models/login.model';
import {AlertService} from '../../services/alert/alert.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isBusy: boolean;
  model: LoginModel;

  constructor(private service: UserService, private alert: AlertService, private router: Router) {
    this.isBusy = false;
    this.model = new LoginModel('', '');
  }

  ngOnInit() {

  }

  login(): void {
    this.isBusy = true;
    this.service.login(this.model).then(payload => {
      if (payload.statusCode === 0) {
        this.router.navigate(['/home']);
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog('existed');
      }
      else {
        this.alert.openDialog('incorrect');
      }
      this.isBusy = false;
    });

  }
}
