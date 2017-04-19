import {Component, OnInit} from '@angular/core';
import {Tag} from "../../models/tag.model";
import {AlertService} from "services/alert/alert.service";
import {AdminService} from "services/admin/admin.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-add-tag',
  templateUrl: './add-tag.component.html',
  styleUrls: ['./add-tag.component.css']
})
export class AddTagComponent implements OnInit {

  isBusy: boolean;
  model: Tag;

  constructor(private adminService: AdminService, private alert: AlertService, private router: Router) {
  }

  ngOnInit() {
    this.model = new Tag('', '');
  }

  async add(): Promise<void> {
    this.isBusy = true;
    try {
      let payload = await this.adminService.addTag(this.model);
      if (payload.statusCode === 0) {
        this.router.navigate(['/manage-tag']);
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog('tag existed');
      }
      else if (payload.statusCode === 2) {
        this.alert.openDialog('incorrect tag info');
      }
      else {
        this.alert.openDialog('unknown code');
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isBusy = false;
    }
  }

}
