import {  NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoanUserRoutingModule } from './loan-user-routing.module';
import { SharedModule } from '../shared/shared.module';
import { LoanUserComponent } from './loan-user/loan-user.component';
import { LoanApplicationComponent } from './loan-application/loan-application.component';


@NgModule({
  declarations: [LoanUserComponent, LoanApplicationComponent],
  imports: [
    CommonModule,
    LoanUserRoutingModule,
    SharedModule
  ]
})
export class LoanUserModule { }
