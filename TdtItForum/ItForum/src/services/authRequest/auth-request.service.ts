import {Injectable} from '@angular/core';
import {AuthHttp} from 'angular2-jwt';
import {Response} from '@angular/http';
import {ReceivedPayload} from '../../dto/receivedPayload.dto';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthRequestService {

  constructor(private http: AuthHttp) {
  }

  post(url: string, body: any): Promise<ReceivedPayload> {
    return this.http.post(url, body).toPromise().then(this.extractData).catch(this.handleError);
  }

  get(url: string): Promise<ReceivedPayload> {
    return this.http.get(url).toPromise().then(this.extractData).catch(this.handleError);
  }

  private extractData(res: Response): ReceivedPayload {
    const json = res.json();
    return json || {};
  }

  private handleError(error: Response | any) {
    // In a real world app, we might use a remote logging infrastructure
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    return Promise.reject(errMsg);
  }

}
