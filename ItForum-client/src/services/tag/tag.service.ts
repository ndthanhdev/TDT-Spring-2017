import { Injectable } from '@angular/core';
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ConstantValuesService} from "../constantValues/constant-values.service";

@Injectable()
export class TagService {

  constructor(private authRequest: AuthRequestService) { }



  async getAllTag(): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_ALL_TAG);
  }

}
