import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class AuthenticationService {

  constructor() { }

  // Observable boolean sources
  private authenticatedSender = new Subject<void>();

  authenticated$ = this.authenticatedSender.asObservable();

  getStatus(): Promise<boolean> {
    return Promise.resolve(true);
  }

  authenticate(): void {
    this.getStatus().then(value => this.authenticatedSender.next());
  }
}
