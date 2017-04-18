import { Component, OnInit } from '@angular/core';
import {Tag} from "../../models/tag.model";
import {AdminService} from "../../services/admin/admin.service";
import {AlertService} from "../../services/alert/alert.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-manage-category',
  templateUrl: './manage-category.component.html',
  styleUrls: ['./manage-category.component.css']
})
export class ManageCategoryComponent implements OnInit {

  isLoading: boolean;
  rows: Tag[];

  constructor(private adminService: AdminService, private alert: AlertService, private router: Router) { }

  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.adminService.getAllTag();
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

}
