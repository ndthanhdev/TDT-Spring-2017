import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from '../../app/home/home.component';
import {RegisterComponent} from '../../app/register/register.component';
import {LoginComponent} from '../../app/login/login.component';
import {ProfileComponent} from "../../app/profile/profile.component";
import {CategoryComponent} from "../../app/category/category.component";
import {ManageUserComponent} from "../../app/manage-user/manage-user.component";
import {ManageCategoryComponent} from "../../app/manage-category/manage-category.component";
import {VerifyPostComponent} from "../../app/verify-post/verify-post.component";
import {AddCategoryComponent} from "../../app/add-category/add-category.component";


const appRoutes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: HomeComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'category', component: CategoryComponent},
  {path: 'manage-user', component: ManageUserComponent},
  {path: 'manage-category', component: ManageCategoryComponent},
  {path: 'manage-category/add', component: AddCategoryComponent},
  {path: 'verify-post', component: VerifyPostComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
