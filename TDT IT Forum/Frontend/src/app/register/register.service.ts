/**
 * Created by thanh on 3/7/2017.
 */
import { Http, Response, Headers, RequestOptions } from '@angular/http'
import { Injectable } from '@angular/core'

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class RegisterService {
  constructor(private http: Http) {
  }

  login(username: String, password: String): void {
    let headers = new Headers({});
    // headers.append('Content-Type', 'application/json');
    // headers.append('Access-Control-Allow-Headers', 'Content-Type');
    // headers.append('Content-Type', 'GET');
    // headers.append('Access-Control-Allow-Methods', '*');
    let options = new RequestOptions({ headers: headers });
    this.http.get('http://localhost:62636/test').subscribe(value => console.log(value.json()));

  }

  private extractData(res: Response) {
    console.log(res);
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
    return Observable.throw(errMsg);
  }

}
