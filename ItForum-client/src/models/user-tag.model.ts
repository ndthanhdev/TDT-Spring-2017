/**
 * Created by duyth on 4/17/2017.
 */
export class UserTag{
  public userId:string;
  public tagId:string;

  constructor(userId: string, tagId: string) {
    this.userId = userId;
    this.tagId = tagId;
  }
}
