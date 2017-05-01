import {Component, OnDestroy, OnInit} from '@angular/core';
import {TopicService} from "../../services/topic/topic.service";
import {Container} from "@angular/compiler/src/i18n/i18n_ast";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {AlertService} from "../../services/alert/alert.service";
import {Tag} from "../../models/tag.model";
import {TagService} from "../../services/tag/tag.service";

@Component({
  selector: 'app-tag-detail',
  templateUrl: './tag-detail.component.html',
  styleUrls: ['./tag-detail.component.css']
})
export class TagDetailComponent implements OnInit, OnDestroy {

  sub: any;
  tagId: string;
  isLoading: boolean;
  rows: Container[];
  tag: Tag;

  constructor(private route: ActivatedRoute, private router: Router, private containerService: TopicService, private tagService:TagService, private alert: AlertService) {

  }

  async ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.tagId = params.id;
      this.reload();
      this.reloadTag();
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.containerService.getContainersInTag(this.tagId);
      if (payload.statusCode === 0) {
        this.rows = payload.data;
      }
      else if (payload.statusCode === 1) {
        this.alert.openNotFoundDialog();
        this.router.navigate(['/home']);
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isLoading = false;
    }
  }

  async reloadTag(): Promise<void> {
    // this.isLoading = true;
    try {
      let payload = await this.tagService.getTagById(this.tagId);
      if (payload.statusCode === 0) {
        this.tag = payload.data;
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog("tag not exist")
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      // this.isLoading = false;
    }
  }

}
