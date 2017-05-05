import {Component, Input, OnInit} from '@angular/core';
import{Comment} from '../../models/comment.model';
import {CommentService} from "../../services/comment/comment.service";
import {AlertService} from "../../services/alert/alert.service";
import {UserService} from "../../services/user/user.service";
import {ConstantValuesService} from "../../services/constantValues/constant-values.service";
@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {

  @Input()
  commentId: string;
  isLiked: boolean;
  isLoading: boolean;
  @Input()
  interactive: boolean;
  model: Comment;

  constructor(private commentService: CommentService, private userService: UserService, private alert: AlertService) {
  }

  ngOnInit() {
    this.interactive = UserService.isAuthorized();
    this.reload();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.commentService.getComment(this.commentId);
      if (payload.statusCode === 0) {
        this.model = payload.data;
      }
      await this.reloadCommentPoint();

    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isLoading = false;
    }
  }

  async like(): Promise<void> {
    let payload = await this.commentService.like(this.model.commentId);
    if (payload.statusCode === 0) {
      this.reloadCommentPoint();
    }
  }


  async reloadCommentPoint(): Promise<void> {
    let payload = await this.commentService.getCommentPoints(this.model.commentId);
    if (payload.statusCode === 0) {
      this.model.commentPoints = payload.data;
      this.isLiked = this.interactive ? this.model.commentPoints.some(pp => pp.userId == this.userService.getJwt()[ConstantValuesService.JWT_USERNAME]) : false;
    }
  }
}
