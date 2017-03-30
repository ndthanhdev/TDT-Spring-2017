import {Injectable} from '@angular/core';

@Injectable()
export class ConstantValuesService {

  public static readonly API = 'http://localhost:62636';
  public static readonly JWT_TOKEN_NAME = 'id_token';
  public static readonly REGISTER_URL = ConstantValuesService.API + '/user/register';
  public static readonly LOGIN_URL = ConstantValuesService.API + '/user/login';
  constructor() {
  }


}
