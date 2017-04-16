import {Component, OnInit} from '@angular/core';
import {AlertService} from '../../services/alert/alert.service';
import {RegisterModel} from '../../models/register.model';
import {UserService} from '../../services/user/user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  admissionYears: number[] = [];
  model: RegisterModel;
  isBusy: boolean;

  constructor(private service: UserService, private alert: AlertService, private router: Router) {
  }

  ngOnInit() {
    const currentYear = (new Date()).getFullYear();
    for (let _i = currentYear; _i >= 1997; _i--) {
      this.admissionYears.push(_i);
    }
    this.model = new RegisterModel('', '', '', '', currentYear, '', '');
  }

  async register(): Promise<void> {
    this.isBusy = true;
    try {
      const payload = await this.service.register(this.model);
      if (payload.statusCode === 0) {
        this.router.navigate(['/login']);
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog('existed');
      }
      else {
        this.alert.openDialog('incorrect');
      }
    } catch (e) {
      this.alert.openDialog(e);
    }
    finally {
      this.isBusy = false;
    }
  }
}


