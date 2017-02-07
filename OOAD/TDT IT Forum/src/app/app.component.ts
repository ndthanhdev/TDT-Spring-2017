import { Component } from '@angular/core';
import { Router } from '@angular/router'
import { AuthenticationService } from "./services/authentication.service"

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [AuthenticationService]
})
export class AppComponent {
  title = 'app works!';

  isAuthorized: boolean = false;

  constructor(router: Router, authenticationService: AuthenticationService) {
    router.events.subscribe((url: any) => console.log(url));
    authenticationService.getStatus().then(value => this.isAuthorized = value);
  }
}
