import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role';

const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./frontpage.component')
      .then(_ => _.FrontpageComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./login.component')
      .then(_ => _.LoginComponent)
  },
  {
    path: 'profile/:id',
    loadComponent: () => import('./profile.component')
      .then(_ => _.ProfileComponent),
    canActivate: [AuthGuard], data: { roles: [Role.User, Role.Admin] }
  },
  {
    path: 'admin/admin-hero',
    loadComponent: () => import('./admin/admin-hero.component')
      .then(_ => _.AdminHeroComponent),
    canActivate: [AuthGuard], data: { roles: [Role.Admin] }
  },
  { path: '**', redirectTo: '/', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
