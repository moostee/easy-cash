import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoansComponent } from '../shared/components/loans/loans.component';
import { AdminComponent } from './admin/admin.component';
import { UsersComponent } from './components/users/users.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      { path: 'loans', component: LoansComponent },
      { path: 'users', component: UsersComponent },
      //{ path: 'dashboard', component: ServicesComponent },
      { path: '', redirectTo: '/admin/loans', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
