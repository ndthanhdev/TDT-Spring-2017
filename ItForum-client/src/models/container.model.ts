import {Post} from "./post.model";
/**
 * Created by duyth on 4/19/2017.
 */
export class Container {
  public postId: string;

  public title: string;

  public containerId: string;

  public post:Post;


  constructor(postId: string, title: string, containerId: string, post: Post) {
    this.postId = postId;
    this.title = title;
    this.containerId = containerId;
    this.post = post;
  }
}
