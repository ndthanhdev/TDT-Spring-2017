import {Component, OnInit} from '@angular/core';
import {AdminService} from "../../services/admin/admin.service";
import {User} from "../../models/user.model";
import {AlertService} from "../../services/alert/alert.service";
import {ancestorWhere} from "tslint";

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.css']
})
export class ManageUserComponent implements OnInit {

  isBusy: boolean;
  rows: User[];
  temp: User[];

  constructor(private adminService: AdminService, private alert: AlertService) {
  }

  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isBusy = true;
    try {
      let payload = await this.adminService.getAllUser();
      if (payload.statusCode === 0) {
        this.rows = payload.data;
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isBusy = false;
    }
  }

  async verify(user: User): Promise<void> {
    (<any>user).isBusy = true;
    try {
      let payload = await this.adminService.verifyUser(user.userId);
      if (payload.statusCode === 0) {
        user.isVerified = !user.isVerified;
      }
    } catch (err) {
      this.alert.openDialog(err);
    } finally {
      (<any>user).isBusy = false;
    }
  }

}
