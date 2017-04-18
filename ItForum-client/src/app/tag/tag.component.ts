import {Component, OnInit} from '@angular/core';
import {AlertService} from "../../services/alert/alert.service";
import {TagService} from "../../services/tag/tag.service";
import {Tag} from "../../models/tag.model";

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.css']
})
export class TagComponent implements OnInit {

  isBusy: boolean;
  categories: Tag[];

  constructor(private tagService: TagService, private alert: AlertService) {
  }

  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isBusy = true;
    try {
      let payload = await this.tagService.getAllTag();
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
