import {Injectable} from '@angular/core';
import {ReceivedPayload} from '../../dto/receivedPayload.dto';
import {RequestService} from '../request/request.service';
import {ConstantValuesService} from '../constantValues/constant-values.service';
import {LoginModel} from '../../models/login.model';
import {RegisterModel} from '../../models/register.model';
import {Subject} from 'rxjs/Subject';
import {JwtHelper} from 'angular2-jwt/angular2-jwt';
import {ProfileModel} from '../../models/profile.model';
@Injectable()
export class UserService {

  profile: ProfileModel;

  // Observable string sources
  private authorizationSource = new Subject<void>();

  // Observable string streams
  authorizationChanged$ = this.authorizationSource.asObservable();

  static isAuthorized(): boolean {
    return localStorage.getItem('id_token') != null;
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
    return this.request.post(ConstantValuesService.REGISTER_URL, userId);
  }

}