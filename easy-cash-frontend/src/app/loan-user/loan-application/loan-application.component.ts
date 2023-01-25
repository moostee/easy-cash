import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Validator } from 'fluentvalidation-ts';
import { throwError, timeout } from 'rxjs';
import { CreateLoan } from 'src/app/core/interfaces/Loan';
import { LoanService } from 'src/app/core/services/loan.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-loan-application',
  templateUrl: './loan-application.component.html',
  styleUrls: ['./loan-application.component.css']
})
export class LoanApplicationComponent implements OnInit {

  newApplicationForm!: FormGroup;
  showSpinner: boolean = false;

  // eligibleAmount: string = "10000";

  loanPercentage: number = 0;

  constructor(private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _loanService: LoanService,
    private _tokenService: TokenStorageService,
    private router: Router) { }

  ngOnInit(): void {

    this.newApplicationForm = this.fb.group({

      loanAmount: new FormControl(0, Validators.compose([
        Validators.required
      ])),

      reason: new FormControl('', Validators.compose([
        Validators.required
      ])),



    }, { validators: loanApplicationValidator });

  }

  createLoan(form: any) {

    var userId = this._tokenService.getUser().id;

    let request: CreateLoan = {
      "loanAmount": parseFloat(form.loanAmount),
      'startDate': new Date().toISOString(),
      'endDate': new Date().toISOString(),
      'userId': userId
    }
    this._loanService.addNewLoan(request).subscribe(result => {

      this.newApplicationForm.reset();
      this._snackBar.open(`Successfull`, 'Ok', {
        duration: 3000
      });


      this.router.navigateByUrl('user');

    }, err => {
      //throwError(err);
      console.log(err);
    });
  }


}



export interface LoanApplicationModel {
  loanAmountError: number;
  reasonError: string;
}

export class FormValidator extends Validator<LoanApplicationModel> {
  constructor() {
    super();

    this.ruleFor('loanAmountError')
      .greaterThanOrEqualTo(1000)
      .withMessage("Loan amount must be greater than 1000");

    this.ruleFor('reasonError')
      .notEmpty()
      .withMessage('Please state a loan reason');
  }
}


export const loanApplicationValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

  const loanAmount = control.get('loanAmount');
  const reason = control.get('reason');

  let fluentValidator = new FormValidator();

  let loginForm: LoanApplicationModel = {
    loanAmountError: parseFloat(loanAmount?.value),
    reasonError: reason?.value
  };

  let isValid = fluentValidator.validate(loginForm);

  if (Object.keys(isValid).length !== 0) {
    return {
      loanAmountError: isValid.loanAmountError,
      reasonError: isValid.reasonError,
      notmatched: true
    }
  }

  return null;

};
