import {Post} from "./post.model";
import {TopicTag} from "./topic-tag.model";
/**
 * Created by duyth on 4/19/2017.
 */
export class Topic {
  public postId: string;

  public title: string;

  public topicId: string;

  public post:Post;

  public posts:Post[];

  public topicTags:TopicTag[];

  constructor(postId: string, title: string, topicId: string, post: Post) {
    this.postId = postId;
    this.title = title;
    this.topicId = topicId;
    this.post = post;
    this.topicTags = new Array<TopicTag>();
  }
}
