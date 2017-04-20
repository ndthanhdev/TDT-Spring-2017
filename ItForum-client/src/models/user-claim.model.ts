export class UserClaim{

  public id:string;
  public userId:string;
  public claimType:string;
  public claimValue:string;

  constructor(id: string, userId: string, claimType: string, claimValue: string) {
    this.id = id;
    this.userId = userId;
    this.claimType = claimType;
    this.claimValue = claimValue;
  }
}
