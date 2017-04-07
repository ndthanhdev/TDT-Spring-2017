/**
 * Created by duyth on 3/28/2017.
 */
export class RegisterModel {
  constructor(public Username: String, public PasswordHash: String, public FullName: String,
              public Faculty: String, public AdmissionYear: Number, public Email: String,
              public Phone: String) {  }
}
