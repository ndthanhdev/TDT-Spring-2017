/**
 * Created by thanh on 3/7/2017.
 */
import {Http, Response, Headers, RequestOptions} from '@angular/http'
import {Injectable} from '@angular/core'

import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import {RegisterInformation} from '../models/registerInformation';
import {ConstanValue} from '../services/constantValue';

@Injectable()
export class RegisterService {

  model: RegisterInformation = new RegisterInformation('thanhlala', 'lala', 'lala', 'lala', 2014, 'lala', '111');

  constructor(private http: Http) {
  }

  login(username: String, password: String): void {
    let headers = new Headers({});
    // headers.append('Content-Type', 'application/json');
    // headers.append('Access-Control-Allow-Headers', 'Content-Type');
    // headers.append('Content-Type', 'GET');
    // headers.append('Access-Control-Allow-Methods', '*');
    let options = new RequestOptions({headers: headers});
    this.http.get('http://localhost:62636/').subscribe(value => console.log(value.json()));

    (this.http.post('http://localhost:62636/user/register', this.model, options)
      .map(this.extractData)
      .catch(this.handleError))
      .subscribe(result => console.log(result), err => console.log(err));

  }

  register(info: RegisterInformation): Promise<boolean> {
    return this.http.post(ConstanValue.API, info).toPromise().then(this.extractData)
      .catch(this.handleError);
  }

  private extractData(res: Response) {
    let body = res.json();
    return body.data || {};
  }

  private handleError(error: Response | any) {
    // In a real world app, we might use a remote logging infrastructure
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Promise.reject(errMsg);
  }

}
//
// class RegisterInfor {
//   constructor(public Username: String, public PasswordHash: String, public FullName: String,
//               public Faculty: String, public AdmissionYear: Number, public Mail: String,
//               public Phone: String) {
//   }
// }
