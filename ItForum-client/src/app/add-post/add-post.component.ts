import {Component, OnInit} from '@angular/core';
import {Container} from "../../models/container.model";
import {Post} from "../../models/post.model";
import {ContainerService} from "../../services/container/container.service";
import {UserService} from "../../services/user/user.service";
import {ConstantValuesService} from "../../services/constantValues/constant-values.service";

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.css']
})
export class AddPostComponent implements OnInit {

  isBusy: boolean;
  model: Container;

  constructor(private containerService: ContainerService,private userService:UserService) {
  }

  ngOnInit() {
    this.model = new Container(null, '', null, new Post(this.userService.getJwt()[ConstantValuesService.JWT_USERNAME]));
  }

  async add() {
    console.log(this.model);
  }
}
