import { Component, EventEmitter, OnInit, Input, Output } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    authenticationService: AuthenticationService;

    constructor(authenticationService: AuthenticationService) {
        this.authenticationService = authenticationService;
    }

    ngOnInit() {
    }

    login(): void {
        this.authenticationService.authenticate();
    }

}
