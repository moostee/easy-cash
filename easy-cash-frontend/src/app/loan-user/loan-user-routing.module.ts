import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoanUserComponent } from './loan-user/loan-user.component';

const routes: Routes = [
  {
    path: '',
    component: LoanUserComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoanUserRoutingModule { }
