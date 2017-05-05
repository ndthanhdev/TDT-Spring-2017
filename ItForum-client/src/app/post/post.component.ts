import {Component, Input, OnInit} from '@angular/core';
import {Post} from "../../models/post.model";
import {PostService} from "../../services/post/post.service";
import {AlertService} from "services/alert/alert.service";
import {UserService} from "../../services/user/user.service";
import {User} from "../../models/user.model";
import {ConstantValuesService} from "../../services/constantValues/constant-values.service";
import {MdDialog} from "@angular/material";
import {ReplyComponent} from "../reply/reply.component";

import{Comment} from '../../models/comment.model';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  @Input()
  model: Post;


  user: User;
  isLoading: boolean;
  @Input()
  interactive: boolean;

  isLiked: boolean;

  currentUser: User;

  constructor(private postService: PostService, private userService: UserService, private alert: AlertService,public dialog: MdDialog) {

  }

  ngOnInit() {
    this.interactive = UserService.isAuthorized();
    this.reload();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.userService.getProfile(this.model.userId);
      if (payload.statusCode === 0) {
        this.user = payload.data;
      }
      await this.reloadPostPoint();
      await this.reloadComments();

    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isLoading = false;
    }
  }

  async reloadPostPoint(): Promise<void> {
    let payload = await this.postService.getPostPoints(this.model.postId);
    if (payload.statusCode === 0) {
      this.model.postPoints = payload.data;
      this.isLiked = this.interactive ? this.model.postPoints.some(pp => pp.userId == this.userService.getJwt()[ConstantValuesService.JWT_USERNAME]) : false;
    }
  }

  async like(): Promise<void> {
    let payload = await this.postService.liekPost(this.model.postId);
    if (payload.statusCode === 0) {
      this.reloadPostPoint();
    }
  }

  async reloadComments(): Promise<void> {
    let payload = await this.postService.getComments(this.model.postId);
    if (payload.statusCode === 0) {
      this.model.comments = payload.data;

    }
  }

  addComment():void{
    let dialogRef = this.dialog.open(ReplyComponent);
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        let comment:Comment = new Comment();
        comment.userId=this.userService.getJwt()[ConstantValuesService.JWT_USERNAME];
        comment.postId=this.model.postId;
        comment.content=result;
        this.postService.addComment(comment);
      }
    });
  }


}
