import {Injectable} from '@angular/core';
import {ReceivedPayload} from '../../dto/receivedPayload.dto';
import {RequestService} from '../request/request.service';
import {ConstantValuesService, RegisteredRoles} from '../constantValues/constant-values.service';
import {LoginModel} from '../../models/login.model';
import {RegisterModel} from '../../models/register.model';
import {Subject} from 'rxjs/Subject';
import {JwtHelper} from 'angular2-jwt/angular2-jwt';
import {User} from "../../models/user.model";


@Injectable()
export class UserService {

  profile: User;

  // Observable string sources
  private authorizationSource = new Subject<void>();

  // Observable string streams
  authorizationChanged$ = this.authorizationSource.asObservable();

  static isAuthorized(): boolean {
    return localStorage.getItem(ConstantValuesService.JWT_TOKEN_NAME) != null;
  }

  constructor(private request: RequestService, private jwtHelper: JwtHelper) {
  }

  // Service message commands
  notifyAuthorizedChanged() {
    this.authorizationSource.next();
  }


  login(loginModel: LoginModel): Promise<ReceivedPayload> {
    return this.request.post(ConstantValuesService.LOGIN_URL, loginModel);
  }

  register(info: RegisterModel): Promise<ReceivedPayload> {
    return this.request.post(ConstantValuesService.REGISTER_URL, info);
  }

  getProfile(userId: string): Promise<ReceivedPayload> {
    return this.request.get(ConstantValuesService.GET_PROFILE_URL + encodeURIComponent(userId));
  }

  getJwt(): any {
    return this.jwtHelper.decodeToken(localStorage.getItem(ConstantValuesService.JWT_TOKEN_NAME));
  }

  getRoles():string[]{
    return this.getJwt()[ConstantValuesService.JWT_ROLE];
  }

  isAdmin(): boolean {
    let jwt = this.getJwt();
    return jwt[ConstantValuesService.JWT_ROLE].indexOf(RegisteredRoles.Adminstrator) !== -1;
  }

  isMod(): boolean {
    return this.getJwt()[ConstantValuesService.JWT_ROLE].indexOf(RegisteredRoles.Moderator) !== -1;
  }

  isUser(): boolean {
    return this.getJwt()[ConstantValuesService.JWT_ROLE].indexOf(RegisteredRoles.User) !== -1;
  }

}
