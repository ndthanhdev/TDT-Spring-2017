/**
 * Created by thanh on 3/2/2017.
 */
export class RegisterInformation {
  constructor(public Username: String, public PasswordHash: String, public FullName: String,
              public Faculty: String, public AdmissionYear: Number, public Mail: String,
              public Phone: String){}
}
