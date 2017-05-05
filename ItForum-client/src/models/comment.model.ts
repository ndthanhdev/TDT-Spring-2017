
import {CommentPoint} from './comment-point.model';

export class Comment{
  public commentId:string;
  public publishDate:Date;
  public content:string;
  public userId:string;
  public postId:string;
  public commentPoints: CommentPoint[];
}
