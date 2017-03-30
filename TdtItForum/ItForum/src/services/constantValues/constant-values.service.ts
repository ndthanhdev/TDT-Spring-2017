import {Injectable} from '@angular/core';

@Injectable()
export class ConstantValuesService {

  public static readonly API = 'http://localhost:62636'; //'http://thanhgular.azurewebsites.net';
  public static readonly JWT_TOKEN_NAME = 'id_token';
  public static readonly REGISTER_URL = ConstantValuesService.API + '/user/register';
  public static readonly LOGIN_URL = ConstantValuesService.API + '/user/login';
  public static readonly JWT_USERNAME = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';

  constructor() {
  }


}
