import {Injectable} from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {ConstantValuesService} from "../constantValues/constant-values.service";
import {Topic} from "../../models/topic.model";
import {RequestService} from "../request/request.service";
import {Post} from "../../models/post.model";

@Injectable()
export class TopicService {

  constructor(private authRequest: AuthRequestService,private request:RequestService) {
  }

  async getContainersInTag(tagId: string): Promise<ReceivedPayload> {
    return this.request.get(ConstantValuesService.GET_TOPICS_IN_TAG + encodeURIComponent(tagId));
  }

  async createContainer(container: Topic): Promise<ReceivedPayload> {
    return this.authRequest.post(ConstantValuesService.CREATE_TOPIC, container);
  }

  async getTopic(topicId: string): Promise<ReceivedPayload> {
    return this.request.get(ConstantValuesService.GET_TOPIC + topicId);
  }
  async addPost(post: Post): Promise<ReceivedPayload> {
    return this.authRequest.post(ConstantValuesService.BASE_URL + `/posts/addpost`, post);
  }
}
