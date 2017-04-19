import {Component, OnInit} from '@angular/core';
import {Container} from "../../models/container.model";
import {Post} from "../../models/post.model";
import {ContainerService} from "../../services/container/container.service";
import {UserService} from "../../services/user/user.service";
import {ConstantValuesService} from "../../services/constantValues/constant-values.service";
import {Tag} from "../../models/tag.model";
import {TagService} from "../../services/tag/tag.service";
import {FormControl} from "@angular/forms";
import {AlertService} from "../../services/alert/alert.service";
import {ContainerTag} from "../../models/container-tag.model";

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.css']
})
export class AddPostComponent implements OnInit {

  isBusy: boolean;
  model: Container;
  tags: Tag[] = [{tagId: '1', description: 'asdqwe', name: 't1'}, {
    tagId: '2',
    description: 'asdqwe',
    name: 't2'
  }, {tagId: '3', description: 'asdqwe', name: 't3'}];

  formC: FormControl;

  constructor(private containerService: ContainerService, private userService: UserService, private tagService: TagService, private alert: AlertService) {
    this.formC = new FormControl();
  }

  ngOnInit() {
    this.model = new Container(null, '', null, new Post(this.userService.getJwt()[ConstantValuesService.JWT_USERNAME]));
  }

  async add() {
    console.log(this.model);
  }

  addTag(tagName: string) {
    for (let tag of this.tags) {
      if (tag.name === tagName) {
        for(let ct of this.model.containerTag){
          if(ct.tagId===tag.tagId){
            this.alert.openSnackbar(`${tag.name} was added`);
            return;
          }
        }
        this.model.containerTag.push(new ContainerTag(tag.tagId));
        return;
      }
    }
    this.alert.openSnackbar(`${tagName} not exist`);
  }

  getTagByName(tagId:string):string{
    return this.tags.filter(t=>t.tagId===tagId)[0].name;
  }
}
