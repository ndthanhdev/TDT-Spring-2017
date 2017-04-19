export class Post {
  public postId: string;

  public content: string;

  public userId: string;

  public containerId: string;

  public isVerified: boolean;

  public publishDate: Date;


  constructor(userId: string) {
    this.userId = userId;
  }
}
