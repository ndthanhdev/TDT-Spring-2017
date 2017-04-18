import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from '../../app/home/home.component';
import {RegisterComponent} from '../../app/register/register.component';
import {LoginComponent} from '../../app/login/login.component';
import {ProfileComponent} from "../../app/profile/profile.component";
import {TagComponent} from "../../app/tag/tag.component";
import {ManageUserComponent} from "../../app/manage-user/manage-user.component";
import {ManageTagComponent} from "../../app/manage-tag/manage-tag.component";
import {VerifyPostComponent} from "../../app/verify-post/verify-post.component";
import {AddCategoryComponent} from "../../app/add-category/add-category.component";
import {TagDetailComponent} from "../../app/tag-detail/tag-detail.component";


const appRoutes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: HomeComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'tag', component: TagComponent},
  {path: 'manage-user', component: ManageUserComponent},
  {path: 'manage-tag', component: ManageTagComponent},
  {path: 'manage-tag/add', component: AddCategoryComponent},
  {path: 'verify-post', component: VerifyPostComponent},
  {path: 'tag/:id', component: TagDetailComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
