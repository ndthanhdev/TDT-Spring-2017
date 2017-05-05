import {Injectable} from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";
import {ConstantValuesService} from "../constantValues/constant-values.service";
import {RequestService} from "../request/request.service";
import{Comment} from '../../models/comment.model';
@Injectable()
export class PostService {

  constructor(private authRequest: AuthRequestService, private request: RequestService) {
  }

  async getAllTag(): Promise<ReceivedPayload> {
    return this.request.get(ConstantValuesService.GET_ALL_TAG);
  }

  async getUnverifyPost(): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.GET_UNVERIFYPOST);
  }

  async verifyPost(postId: String): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.VERIFY_POST + `/${postId}`);
  }

  async getPostPoints(postId: String): Promise<ReceivedPayload> {
    return this.request.get(ConstantValuesService.BASE_URL + `/posts/getpostpoints/${postId}`);
  }

  async liekPost(postId: String): Promise<ReceivedPayload> {
    return this.authRequest.get(ConstantValuesService.BASE_URL + `/posts/likepost/${postId}`);
  }

  async getComments(postId: String): Promise<ReceivedPayload> {
    return this.request.get(ConstantValuesService.BASE_URL + `/comment/getcomments/${postId}`);
  }

  async addComment(comment: Comment): Promise<ReceivedPayload> {
    return this.authRequest.post(ConstantValuesService.BASE_URL + `/comment/AddComment`, comment);
  }
}
