import {Injectable} from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {ConstantValuesService} from "../constantValues/constant-values.service";
import {Topic} from "../../models/topic.model";

@Injectable()
export class TopicService {

  constructor(private authRequest: AuthRequestService) {
  }

  async getContainersInTag(tagId: string): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_TOPICS_IN_TAG + encodeURIComponent(tagId));
  }

  async createContainer(container: Topic): Promise<ReceivedPayload> {
    return this.authRequest.post(ConstantValuesService.CREATE_TOPIC, container);
  }
}
