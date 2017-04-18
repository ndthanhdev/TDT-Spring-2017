import {Component, OnInit} from '@angular/core';
import {AlertService} from "../../services/alert/alert.service";
import {CategoryService} from "../../services/category/category.service";
import {Tag} from "../../models/tag.model";

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  isBusy: boolean;
  categories: Tag[];

  constructor(private categoryService: CategoryService, private alert: AlertService) {
  }

  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isBusy = true;
    try {
      let payload = await this.categoryService.getAllTag();
      if (payload.statusCode === 0) {
        this.categories = payload.data;
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
