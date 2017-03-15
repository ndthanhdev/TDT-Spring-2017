import { Component } from '@angular/core';
import { Router } from '@angular/router'
import { AuthenticationService } from "./services/authentication.service"

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})
export class AppComponent {

  isAuthorized: boolean = false;


  constructor(router: Router, private authenticationService: AuthenticationService) {
    router.events.subscribe((url: any) => console.log(url));
    authenticationService.authenticated$.subscribe(value => this.updateAuthorization());
  }

  private updateAuthorization(): void {
    this.authenticationService.getStatus().then(value => this.isAuthorized = value);
  }

}
