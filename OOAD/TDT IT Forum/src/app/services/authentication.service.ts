import { Injectable } from '@angular/core';

@Injectable()
export class AuthenticationService {

  constructor() { }
  getStatus(): Promise<boolean> {
    return Promise.resolve(false);
  }
}
