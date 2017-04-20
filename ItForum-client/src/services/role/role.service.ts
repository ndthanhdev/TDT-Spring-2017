import { Injectable } from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {ConstantValuesService} from "../constantValues/constant-values.service";
import {User} from "../../models/user.model";

@Injectable()
export class RoleService {

  constructor(private authRequest: AuthRequestService) { }

  async UpdateUserClaimOfUser(user: User): Promise<ReceivedPayload> {
    return this.authRequest.post(ConstantValuesService.UPDATE_USER_CLAIM_OF_USER, user);
  }

}
