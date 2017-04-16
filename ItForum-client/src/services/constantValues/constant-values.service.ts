import {Injectable} from '@angular/core';

@Injectable()
export class ConstantValuesService {

  public static readonly API = 'http://localhost:49957'; //'http://api-thanhgular.azurewebsites.net';
  public static readonly JWT_TOKEN_NAME = 'id_token';
  public static readonly REGISTER_URL = ConstantValuesService.API + '/user/register';
  public static readonly LOGIN_URL = ConstantValuesService.API + '/user/login';
  public static readonly GET_PROFILE_URL = ConstantValuesService.API + '/user/getprofile/';
  public static readonly JWT_USERNAME = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
  public static readonly JWT_ROLE = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';


  constructor() {
  }


}

export class RegisteredRoles {
  public static readonly User = 'User';
  public static readonly Moderator = "Moderator";
  public static readonly Adminstrator = "Adminstrator";
}
