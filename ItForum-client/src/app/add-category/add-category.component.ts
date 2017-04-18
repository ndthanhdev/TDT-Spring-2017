import {Component, OnInit} from '@angular/core';
import {Tag} from "../../models/tag.model";
import {AlertService} from "services/alert/alert.service";
import {AdminService} from "services/admin/admin.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  isBusy: boolean;
  model: Tag;

  constructor(private adminService: AdminService, private alert: AlertService, private router: Router) {
  }

  ngOnInit() {
    this.model = new Tag(null, '', '');
  }

  async add():Promise<void>{
    this.isBusy = true;
    try {
      let payload = await this.adminService.addTag(this.model);
      if (payload.statusCode === 0) {
        this.router.navigate(['/manage-category']);
      }
      else if(payload.statusCode === 1){
        this.alert.openDialog('category existed');
      }
      else if(payload.statusCode === 2){
        this.alert.openDialog('incorrect category info');
      }
      else{
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
