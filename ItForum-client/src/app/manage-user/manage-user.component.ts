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

  isLoading: boolean;
  isAutoVerifing:boolean;
  rows: User[];

  constructor(private adminService: AdminService, private alert: AlertService) {
  }

  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
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
      this.isLoading = false;
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

  async autoVerify():Promise<void>{
    this.isAutoVerifing = true;
    try {
      let payload = await this.adminService.verifyUserAuto();
      if (payload.statusCode === 0) {
        this.alert.openSnackbar(`verified ${payload.data} user`)
        await this.reload();
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isAutoVerifing = false;
    }
  }
}
