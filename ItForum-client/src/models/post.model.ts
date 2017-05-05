import {PostPoint} from "./post-point.model";
import {Comment} from "./comment.model";

export class Post {
  public postId: string;

  public content: string;

  public userId: string;

  public topicId: string;

  public isVerified: boolean;

  public publishDate: Date;

  public postPoints:PostPoint[];

  public comments:Comment[];

  constructor(userId: string) {
    this.userId = userId;
  }
}
