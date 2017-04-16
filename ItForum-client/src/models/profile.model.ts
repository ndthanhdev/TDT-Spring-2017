/**
 * Created by duyth on 3/31/2017.
 */
export class ProfileModel {
  constructor(public userId: string, public username: string, public fullName: string, public faculty: string, public addmissionYear: number,
              public email: string, public phone: string, public isVerified: boolean, public managedTagIds: string[]) {
  }
}
