/**
 * Created by duyth on 4/17/2017.
 */
export class UserTag{
  public userId:string;
  public tagName:string;


  constructor(userId: string, tagName: string) {
    this.userId = userId;
    this.tagName = tagName;
  }
}
