import {Component, OnDestroy, OnInit} from '@angular/core';
import {Container} from "../../models/container.model";
import {Tag} from "../../models/tag.model";
import {ActivatedRoute, Router} from "@angular/router";
import {ContainerService} from "../../services/container/container.service";
import {TagService} from "../../services/tag/tag.service";
import {AlertService} from "../../services/alert/alert.service";

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent implements OnInit {


  constructor(private route: ActivatedRoute, private router: Router,
              private containerService: ContainerService,private tagService:TagService, private alert: AlertService) {

  }

  ngOnInit() {
  }
}
