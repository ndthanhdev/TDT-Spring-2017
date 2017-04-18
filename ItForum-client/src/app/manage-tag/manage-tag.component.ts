import { Component, OnInit } from '@angular/core';
import {Tag} from "../../models/tag.model";
import {AlertService} from "../../services/alert/alert.service";
import {Router} from "@angular/router";
import {TagService} from "../../services/tag/tag.service";

@Component({
  selector: 'app-tag-category',
  templateUrl: './manage-tag.component.html',
  styleUrls: ['./manage-tag.component.css']
})
export class ManageTagComponent implements OnInit {

  isLoading: boolean;
  rows: Tag[];

  constructor(private tagService:TagService, private alert: AlertService, private router: Router) { }

  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.tagService.getAllTag();
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
