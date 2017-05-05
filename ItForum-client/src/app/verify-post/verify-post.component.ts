import {Component, OnInit} from '@angular/core';
import {Post} from "../../models/post.model";
import {PostService} from "../../services/post/post.service";
import {AlertService} from "../../services/alert/alert.service";

@Component({
  selector: 'app-verify-post',
  templateUrl: './verify-post.component.html',
  styleUrls: ['./verify-post.component.css']
})
export class VerifyPostComponent implements OnInit {

  isLoading: boolean;

  constructor(private postService: PostService, private alert: AlertService) {
  }

  posts: Post[];


  ngOnInit() {
    this.reload();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.postService.getUnverifyPost();
      if (payload.statusCode === 0) {
        this.posts = payload.data;
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isLoading = false;
    }
  }

  async verifyPost(model:Post): Promise<void> {
    (<any>model).isVerifying = true;
    try {
      let payload = await this.postService.verifyPost(model.postId);
      if (payload.statusCode === 0) {
        model.isVerified = !model.isVerified;
      }
    } catch (err) {
      this.alert.openDialog(err);
    } finally {
      (<any>model).isVerifying =  false;
    }
  }


}
