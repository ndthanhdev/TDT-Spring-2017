import {Injectable} from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ConstantValuesService} from "../constantValues/constant-values.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {Tag} from "../../models/tag.model";

@Injectable()
export class AdminService {

  constructor(private authRequest: AuthRequestService) {
  }

  async getAllUser(): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_ALL_USER);
  }

  async verifyUser(userId: string): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.VERIFY_USER + userId);
  }

  async verifyUserAuto(): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.VERIFY_USER_AUTO);
  }

  async addTag(tag:Tag):Promise<ReceivedPayload> {
    return this.authRequest.post(ConstantValuesService.ADD_TAG,tag);
  }

}
