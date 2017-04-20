import {Injectable} from '@angular/core';

@Injectable()
export class ConstantValuesService {

  public static readonly BASE_URL = 'http://localhost:49957'; // 'http://api-thanhgular.azurewebsites.net';
  public static readonly JWT_TOKEN_NAME = 'token';

  public static readonly JWT_USERNAME = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
  public static readonly JWT_ROLE = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

  // user
  public static readonly REGISTER_URL = ConstantValuesService.BASE_URL + '/user/register';
  public static readonly LOGIN_URL = ConstantValuesService.BASE_URL + '/user/login';
  public static readonly GET_PROFILE_URL = ConstantValuesService.BASE_URL + '/user/getprofile/';
  public static readonly GET_ALL_USER = ConstantValuesService.BASE_URL + '/user/getalluser';
  public static readonly VERIFY_USER = ConstantValuesService.BASE_URL + '/user/verifyuser/';
  public static readonly VERIFY_USER_AUTO = ConstantValuesService.BASE_URL + '/user/verifyuserauto';

  // tag
  public static readonly GET_ALL_TAG = ConstantValuesService.BASE_URL + '/tag/getalltag';
  public static readonly GET_TAG_BY_ID = ConstantValuesService.BASE_URL + '/tag/GetTagById/';
  public static readonly ADD_TAG = ConstantValuesService.BASE_URL + '/tag/create';
  public static readonly UPDATE_USERTAG_OF_USER = ConstantValuesService.BASE_URL + '/tag/UpdateUserTagOfUser';

  // container
  public static readonly GET_CONTAINERS_IN_TAG = ConstantValuesService.BASE_URL + '/container/GetContainersInTag/';
  public static readonly CREATE_CONTAINERS = ConstantValuesService.BASE_URL + '/container/Create';

  //role
  public static readonly UPDATE_USER_CLAIM_OF_USER = ConstantValuesService.BASE_URL + '/UserClaim/UpdateUserClaimOfUser';

  constructor() {
  }


}

export class RegisteredRoles {
  public static readonly User = 'User';
  public static readonly Moderator = "Moderator";
  public static readonly Adminstrator = "Adminstrator";
}
