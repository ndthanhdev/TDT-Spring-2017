import { Component, OnInit } from '@angular/core';
import {Tag} from "../../models/tag.model";
import {AlertService} from "../../services/alert/alert.service";
import {Router} from "@angular/router";
import {CategoryService} from "../../services/category/category.service";

@Component({
  selector: 'app-manage-category',
  templateUrl: './manage-category.component.html',
  styleUrls: ['./manage-category.component.css']
})
export class ManageCategoryComponent implements OnInit {

  isLoading: boolean;
  rows: Tag[];

  constructor(private categoryService:CategoryService, private alert: AlertService, private router: Router) { }

  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.categoryService.getAllTag();
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
