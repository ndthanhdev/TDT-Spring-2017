import {Component, OnInit} from '@angular/core';
import {Topic} from "../../models/topic.model";
import {Post} from "../../models/post.model";
import {TopicService} from "../../services/topic/topic.service";
import {UserService} from "../../services/user/user.service";
import {ConstantValuesService} from "../../services/constantValues/constant-values.service";
import {Tag} from "../../models/tag.model";
import {TagService} from "../../services/tag/tag.service";
import {FormControl} from "@angular/forms";
import {AlertService} from "../../services/alert/alert.service";
import {TopicTag} from "../../models/topic-tag.model";
import {Router} from "@angular/router";

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.css']
})
export class AddPostComponent implements OnInit {

  isBusy: boolean;
  model: Topic;
  tags: Tag[];

  formC: FormControl;
  filteredTags: any;

  constructor(private containerService: TopicService,
              private userService: UserService,
              private tagService: TagService,
              public alert: AlertService,
              private router: Router) {
    this.formC = new FormControl();
    this.filteredTags = this.formC.valueChanges
      .startWith(null)
      .map(name => this.filterTags(name));
  }

  ngOnInit() {
    this.model = new Topic(null, '', null, new Post(this.userService.getJwt()[ConstantValuesService.JWT_USERNAME]));
    this.reload();
  }

  filterTags(val: string) {
    return val ? this.tags.filter(tag => tag.name.indexOf(val) >= 0)
      : this.tags;
  }

  addTag(tagName: string) {
    for (let tag of this.tags) {
      if (tag.name === tagName) {
        for (let ct of this.model.topicTags) {
          if (ct.tagName === tagName) {
            this.alert.openSnackbar(`${tag.name} was added`);
            return;
          }
        }
        this.model.topicTags.push(new TopicTag(tag.name));
        return;
      }
    }
    this.alert.openSnackbar(`${tagName} not exist`);
  }

  removeTag(tagName: string) {
    this.model.topicTags = this.model.topicTags.filter((value, index, array) => value.tagName !== tagName);
  }

  async reload(): Promise<void> {
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

  async add() {
    this.isBusy = true;
    try {
      let payload = await this.containerService.createContainer(this.model);
      if (payload.statusCode === 0) {
        this.router.navigate(['/manage-tag']);
      }
      else if (payload.statusCode === 1) {
        this.alert.openDialog('Invalid post');
      }
      else if (payload.statusCode === 2) {
        this.alert.openDialog('Invalid topic');
      }
      else {
        this.alert.openDialog('unknown code');
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
