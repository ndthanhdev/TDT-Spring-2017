import { Injectable } from '@angular/core';
import {AuthRequestService} from "../authRequest/auth-request.service";
import {RequestService} from "../request/request.service";
import {ConstantValuesService} from "../constantValues/constant-values.service";
import {ReceivedPayload} from "../../dto/receivedPayload.dto";

@Injectable()
export class CommentService {

  constructor(private authRequest: AuthRequestService, private request: RequestService) { }

  async getComment(commentId:string): Promise<ReceivedPayload> {
    return this.request.get(`${ConstantValuesService.BASE_URL}/comment/GetComment/${commentId}`);
  }

  async like(commentId:string): Promise<ReceivedPayload> {
    return this.authRequest.get(`${ConstantValuesService.BASE_URL}/comment/LikeComment/${commentId}`);
  }

  async getCommentPoints(commentId: String): Promise<ReceivedPayload> {
    return this.request.get(ConstantValuesService.BASE_URL + `/comment/getcommentpoints/${commentId}`);
  }
}
