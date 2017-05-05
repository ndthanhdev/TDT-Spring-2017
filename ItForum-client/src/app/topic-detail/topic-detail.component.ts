import {Component, OnDestroy, OnInit} from '@angular/core';
import {AlertService} from "../../services/alert/alert.service";
import {ActivatedRoute} from "@angular/router";
import {TopicService} from "../../services/topic/topic.service";
import {Topic} from "../../models/topic.model";
import {MdDialog} from "@angular/material";
import {ReplyComponent} from "../reply/reply.component";
import {AlertComponent} from "app/alert/alert.component";
import {Post} from "../../models/post.model";
import {UserService} from "../../services/user/user.service";
import {ConstantValuesService} from "../../services/constantValues/constant-values.service";

@Component({
  selector: 'app-topic-detail',
  templateUrl: './topic-detail.component.html',
  styleUrls: ['./topic-detail.component.css']
})
export class TopicDetailComponent implements OnInit, OnDestroy {

  sub: any;
  topicId: string;
  isLoading: boolean;
  topic:Topic;

  constructor(private userService:UserService,private topicService: TopicService,public dialog: MdDialog, private route: ActivatedRoute, private alert: AlertService) {
  }

  async ngOnInit() {

    this.sub = this.route.params.subscribe(params => {
      this.topicId = params.id;
      this.reload();
    });
  }


  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.topicService.getTopic(this.topicId);
      if (payload.statusCode === 0) {
        this.topic = payload.data;
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isLoading = false;
    }
  }

  answer():void{
    let dialogRef = this.dialog.open(ReplyComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
        if(result){
          let post:Post = new Post(this.userService.getJwt()[ConstantValuesService.JWT_USERNAME]);
          post.topicId=this.topicId;
          post.content=result;
          this.topicService.addPost(post);
        }
    });
  }

}
