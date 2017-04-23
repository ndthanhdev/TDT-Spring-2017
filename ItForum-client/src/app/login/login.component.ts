import {Component, OnInit} from '@angular/core';
import {UserService} from '../../services/user/user.service';
import {LoginModel} from '../../models/login.model';
import {AlertService} from '../../services/alert/alert.service';
import {Router} from '@angular/router';
import {ConstantValuesService} from '../../services/constantValues/constant-values.service';
import {MdSnackBar} from "@angular/material";

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

  async login(): Promise<void> {
    this.isBusy = true;
    try {
      const payload = await this.service.login(this.model);
      if (payload.statusCode === 0) {
        localStorage.setItem(ConstantValuesService.JWT_TOKEN_NAME, payload.data);
        this.service.notifyAuthorizedChanged();
        this.alert.openSnackbar("You logged");
        this.router.navigate(['/home']);
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog("user isn't exist");
      }
      else {
        this.alert.openDialog('incorrect');
      }
    }
    catch (e) {
      this.alert.openDialog(e);
    }
    finally {
      this.isBusy = false;
    }


  }
}
