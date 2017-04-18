/**
 * Created by duyth on 4/19/2017.
 */
export class container {
  public postId: string;

  public content: string;

  public userId: string;

  public containerId: string;

  public isVerified: boolean;

  public publishDate: Date;

  constructor(postId: string, content: string, userId: string, containerId: string, isVerified: boolean, publishDate: Date) {
    this.postId = postId;
    this.content = content;
    this.userId = userId;
    this.containerId = containerId;
    this.isVerified = isVerified;
    this.publishDate = publishDate;
  }
}
