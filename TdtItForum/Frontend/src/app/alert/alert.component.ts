import {Component, Input} from '@angular/core';
import { NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',

})
export class AlertComponent {

  @Input() message;
  @Input() title;

  constructor(public activeModal: NgbActiveModal) {}

}
