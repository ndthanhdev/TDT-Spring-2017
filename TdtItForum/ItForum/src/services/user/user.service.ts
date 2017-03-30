import {Injectable} from '@angular/core';
import {ReceivedPayload} from '../../dto/receivedPayload.dto';
import {RequestService} from '../request/request.service';
import {ConstantValuesService} from '../constantValues/constant-values.service';
import {LoginModel} from '../../models/login.model';
import {RegisterModel} from '../../models/register.model';

@Injectable()
export class UserService {

  constructor(private request: RequestService) {
  }

  login(loginModel: LoginModel): Promise<ReceivedPayload> {
    return this.request.post(ConstantValuesService.LOGIN_URL, loginModel);
  }

  register(info: RegisterModel): Promise<ReceivedPayload> {
    return this.request.post(ConstantValuesService.REGISTER_URL, info);
  }
}
