import { Injectable } from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {ConstantValuesService} from "../constantValues/constant-values.service";

@Injectable()
export class PostService {

  constructor(private authRequest: AuthRequestService) { }

  async getAllTag(): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_ALL_TAG);
  }
}
