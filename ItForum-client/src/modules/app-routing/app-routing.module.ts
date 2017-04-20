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
import {AddTagComponent} from "../../app/add-tag/add-tag.component";
import {TagDetailComponent} from "../../app/tag-detail/tag-detail.component";
import {AddPostComponent} from "../../app/add-post/add-post.component";
import {ContainerDetailComponent} from "../../app/container-detail/container-detail.component";
import {TopicDetailComponent} from "../../app/topic-detail/topic-detail.component";


const appRoutes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: HomeComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'profile/:id', component: ProfileComponent},
  {path: 'tag', component: TagComponent},
  {path: 'manage-user', component: ManageUserComponent},
  {path: 'manage-tag', component: ManageTagComponent},
  {path: 'manage-tag/add', component: AddTagComponent},
  {path: 'verify-post', component: VerifyPostComponent},
  {path: 'tag/:id', component: TagDetailComponent},
  {path: 'add-post', component: AddPostComponent},
  {path: 'container-detail/:id', component: ContainerDetailComponent},
  {path: 'topic-detail', component: TopicDetailComponent},
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
