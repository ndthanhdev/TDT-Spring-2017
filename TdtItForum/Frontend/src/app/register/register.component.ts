import {Component, OnInit} from '@angular/core';
import {RegisterInformation} from '../../models/registerInformation';
import {RegisterService} from '../../services/register.service';
import {ConstanValue} from "../../services/constantValue";
import {Router} from "@angular/router";
import {MdDialog} from'@angular/material';
import {AlertComponent} from '../alert/alert.component';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

@Component({
  moduleId: module.id,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  admissionYears: number[] = [];
  model: RegisterInformation;

  constructor(private service: RegisterService, private router: Router, private modalService: NgbModal, public dialog: MdDialog) {
  }

  ngOnInit() {
    let currentYear = (new Date()).getFullYear();
    for (let _i = currentYear; _i >= 1997; _i--) {
      this.admissionYears.push(_i);
    }
    this.model = new RegisterInformation('', '', '', '', currentYear, '', '');
  }

  onSubmit(content): void {
    this.service.register(this.model).then(payload => {
      if (payload.statusCode == 0) {
        this.saveToken(payload.data);
        this.router.navigate(['/home']);
      }
      else {
        const modalRef = this.modalService.open(AlertComponent);
        modalRef.componentInstance.title = 'Register failed';
        if (payload.statusCode == 1)
          modalRef.componentInstance.message = 'Existed';
        else
          modalRef.componentInstance.message = 'Incorrect info';
      }
    }, this.httpError);

  }

  saveToken(token: any): void {
    localStorage.setItem(ConstanValue.JWT_TOKEN_NAME, token);
  }

  httpError(err) {
    console.log(err);
  }

}
