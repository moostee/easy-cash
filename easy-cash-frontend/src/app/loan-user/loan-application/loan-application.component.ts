import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { throwError } from 'rxjs';
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

  eligibleAmount: string = "10000";

  loanPercentage: number = 0;

  constructor(private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _loanService: LoanService,
    private _tokenService: TokenStorageService) { }

  ngOnInit(): void {

    this.newApplicationForm = this.fb.group({

      loanAmount: new FormControl(0, Validators.compose([
        Validators.required
      ])),
      reason: new FormControl('', Validators.compose([
      ])),



    })

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
      })
    }, err => {
      //throwError(err);
      console.log(err);
    });
  }


}
