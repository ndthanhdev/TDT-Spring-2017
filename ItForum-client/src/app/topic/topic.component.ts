import {Component, OnDestroy, OnInit} from '@angular/core';
import {Topic} from "../../models/topic.model";
import {Tag} from "../../models/tag.model";
import {ActivatedRoute, Router} from "@angular/router";
import {TopicService} from "../../services/topic/topic.service";
import {TagService} from "../../services/tag/tag.service";
import {AlertService} from "../../services/alert/alert.service";

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.css']
})
export class TopicComponent implements OnInit {


  constructor(private route: ActivatedRoute, private router: Router,
              private containerService: TopicService, private tagService:TagService, private alert: AlertService) {

  }

  ngOnInit() {
  }
}
