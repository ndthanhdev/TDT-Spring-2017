import {UserTag} from "./user-tag.model";
import {UserClaim} from "./user-claim.model";
/**
 * Created by duyth on 3/31/2017.
 */
export class User {
  constructor(userId: string, username: string, fullName: string, faculty: string, admissionYear: number, email: string, phone: string, isVerified: boolean, userTags: UserTag[]) {
    this.userId = userId;
    this.username = username;
    this.fullName = fullName;
    this.faculty = faculty;
    this.admissionYear = admissionYear;
    this.email = email;
    this.phone = phone;
    this.isVerified = isVerified;
    this.userTags = userTags;
  }

  public userId: string;
  public username: string;
  public fullName: string;
  public faculty: string;
  public admissionYear: number;
  public email: string; public phone: string;
  public isVerified: boolean;
  public userTags: UserTag[];
  public userClaims:UserClaim[];

}
