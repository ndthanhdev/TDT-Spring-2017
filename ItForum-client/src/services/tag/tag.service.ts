import {Injectable} from '@angular/core';
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ConstantValuesService} from "../constantValues/constant-values.service";
import {User} from "../../models/user.model";

@Injectable()
export class TagService {

  constructor(private authRequest: AuthRequestService) {
  }


  async getAllTag(): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_ALL_TAG);
  }

  async getTagById(id: string): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_TAG_BY_ID + encodeURIComponent(id));
  }

  async updateUserTagOfUser(user: User): Promise<ReceivedPayload> {
    return this.authRequest.post(ConstantValuesService.UPDATE_USERTAG_OF_USER, user);
  }

}
