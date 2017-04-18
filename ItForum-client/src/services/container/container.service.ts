import {Injectable} from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {ConstantValuesService} from "../constantValues/constant-values.service";

@Injectable()
export class ContainerService {

  constructor(private authRequest: AuthRequestService) {
  }

  async getContainersInTag(tagId: string): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_CONTAINERS_IN_TAG + tagId);
  }
}
