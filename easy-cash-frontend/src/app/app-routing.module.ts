import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { Role } from "./core/interfaces/Role";

const routes: Routes = [
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canActivate: [AuthGuard],
    data: {
      role: Role.Admin
    }
  },
  {
    path: 'user',
    loadChildren: () => import('./loan-user/loan-user.module').then(m => m.LoanUserModule),
    canActivate: [AuthGuard],
    data: {
      role: Role.LoanUser
    }
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
