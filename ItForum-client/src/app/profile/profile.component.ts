import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user/user.service";
import {User} from "../../models/user.model";
import {AlertService} from "../../services/alert/alert.service";
import {ActivatedRoute} from "@angular/router";
import {Tag} from "../../models/tag.model";
import {TagService} from "../../services/tag/tag.service";
import {UserTag} from "../../models/user-tag.model";
import {FormControl} from "@angular/forms";
import {ConstantValuesService, RegisteredRoles} from "../../services/constantValues/constant-values.service";
import {UserClaim} from "../../models/user-claim.model";
import {RoleService} from "../../services/role/role.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  sub: any;

  model: User;
  isBusy: boolean;
  isLoading:boolean;
  userId: string;

  isAdmin: boolean;


  formRoles: FormControl;
  roles: string[] = [RegisteredRoles.User, RegisteredRoles.Moderator, RegisteredRoles.Adminstrator];
  filteredRoles: any;

  tags: Tag[];
  formTag: FormControl;
  filteredTags: any;

  constructor(private route: ActivatedRoute, public userService: UserService, private tagService: TagService,
    private roleService:RoleService, private alert: AlertService) {
    this.formRoles = new FormControl();
    this.filteredRoles = this.formRoles.valueChanges
      .startWith(null)
      .map(name => this.filterRoles(name));

    this.formTag = new FormControl();
    this.filteredTags = this.formTag.valueChanges
      .startWith(null)
      .map(name => this.filterTags(name));
  }

  async ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.userId = params.id;
      this.reload();
      this.reloadTag();
    });
    this.isAdmin = this.userService.isAdmin();
  }

  async reload(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.userService.getProfile(this.userId);
      if (payload.statusCode === 0) {
        this.model = payload.data;
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog('user not exist');
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isLoading = false;
    }
  }

  async reloadTag(): Promise<void> {
    this.isLoading = true;
    try {
      let payload = await this.tagService.getAllTag();
      if (payload.statusCode === 0) {
        this.tags = payload.data;
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isLoading = false;
    }
  }

  addTag(tagName: string) {
    for (let tag of this.tags) {
      if (tag.name === tagName) {
        for (let ut of this.model.userTags) {
          if (ut.tagName === tagName) {
            this.alert.openSnackbar(`${tag.name} was added`);
            return;
          }
        }
        this.model.userTags.push(new UserTag(this.userId, tag.name));
        return;
      }
    }
    this.alert.openSnackbar(`${tagName} not exist`);
  }

  removeTag(tagName: string) {
    this.model.userTags = this.model.userTags.filter((value, index, array) => value.tagName !== tagName);
  }

  filterTags(val: string) {
    return val ? this.tags.filter(tag => tag.name.indexOf(val) >= 0)
      : this.tags;
  }

  addRole(roleName: string) {
    for (let role of this.roles) {
      if (role === roleName) {
        for (let uc of this.model.userClaims) {
          if (uc.claimType === ConstantValuesService.JWT_ROLE && uc.claimValue === roleName) {
            this.alert.openSnackbar(`${role} was added`);
            return;
          }
        }
        this.model.userClaims.push(new UserClaim(null, this.userId, ConstantValuesService.JWT_ROLE, roleName));
        return;
      }
    }
    this.alert.openSnackbar(`${roleName} not exist`);
  }

  removeClaim(role: string) {
    this.model.userClaims = this.model.userClaims.filter((value, index, array) =>  value.claimValue !== role);
  }

  filterRoles(val: string) {
    return val ? this.roles.filter(role => role.toLocaleLowerCase().indexOf(val.toLocaleLowerCase()) >= 0)
      : this.roles;
  }

  async saveTags() {
    this.isBusy = true;
    try {
      let payload = await this.tagService.updateUserTagOfUser(this.model);
      if (payload.statusCode === 0) {
        // await this.ngOnInit();
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog('tags not exist');
      }
      else if (payload.statusCode === 2) {
        this.alert.openDialog('user not exist');
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isBusy = false;
    }
  }

  async saveRoles() {
    this.isBusy = true;
    try {
      let payload = await this.roleService.UpdateUserClaimOfUser(this.model);
      if (payload.statusCode === 0) {
        // await this.ngOnInit();
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog('user not exist');
      }
    }
    catch (err) {
      this.alert.openDialog(err);
    }
    finally {
      this.isBusy = false;
    }
  }

  async save(){
    await this.saveTags();
    await this.saveRoles();
    await this.ngOnInit();
  }

}
