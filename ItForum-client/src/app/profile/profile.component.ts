import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user/user.service";
import {User} from "../../models/user.model";
import {AlertService} from "../../services/alert/alert.service";
import {ActivatedRoute} from "@angular/router";
import {Tag} from "../../models/tag.model";
import {TagService} from "../../services/tag/tag.service";
import {UserTag} from "../../models/user-tag.model";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  sub: any;

  model: User;
  isBusy: boolean;
  userId: string;

  isAdmin: boolean;

  tags: Tag[];

  formC: FormControl;
  filteredTags: any;

  constructor(private route: ActivatedRoute, public userService: UserService, private tagService: TagService, private alert: AlertService) {
    this.formC = new FormControl();
    this.filteredTags = this.formC.valueChanges
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
    this.isBusy = true;
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
      this.isBusy = false;
    }
  }

  async reloadTag(): Promise<void> {
    this.isBusy = true;
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
      this.isBusy = false;
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

  async save(){
    this.isBusy = true;
    try {
      let payload = await this.tagService.updateUserTagOfUser(this.model);
      if (payload.statusCode === 0) {
        await this.ngOnInit();
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

}
