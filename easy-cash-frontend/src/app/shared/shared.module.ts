import { FlexLayoutModule } from '@angular/flex-layout';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ToolbarComponent } from "./components/toolbar/toolbar.component";
import { MaterialModule } from "./material/material.module";
import { LoansComponent } from './components/loans/loans.component';
import { LoanDetailsComponent } from './components/loan-details/loan-details.component';
import { LayoutModule } from '@angular/cdk/layout';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';

@NgModule({
    declarations: [ToolbarComponent, LoansComponent, LoanDetailsComponent, SideNavComponent],
    imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MaterialModule
    ],
    exports: [
      FormsModule,
      ReactiveFormsModule,
      MaterialModule,
      LayoutModule,
      FlexLayoutModule,
      ToolbarComponent, 
      LoansComponent, 
      LoanDetailsComponent,
      SideNavComponent
    ],
    providers: [
      {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'fill'}}
    ]
  })
  export class SharedModule { }