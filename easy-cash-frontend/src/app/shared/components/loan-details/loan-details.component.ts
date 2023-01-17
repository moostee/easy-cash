import { Component, Input, OnInit } from '@angular/core';
import { Loan, LoanRepayment, Loans } from 'src/app/core/interfaces/Loan';
import { LoanRepaymentService } from 'src/app/core/services/loan-repayment.service';

@Component({
  selector: 'app-loan-details',
  templateUrl: './loan-details.component.html',
  styleUrls: ['./loan-details.component.css']
})
export class LoanDetailsComponent implements OnInit {

  @Input() loan: Partial<Loans> = {};

  loanRepayment: LoanRepayment[] = [];

  displayedColumns: string[] = ['amount', 'duedate', 'status'];

  constructor(private _loanRepaymentService: LoanRepaymentService) { }

  ngOnInit(): void {
    this._loanRepaymentService.getLoanRepaymentByLoanId(this.loan.id).subscribe(result => {
      console.log(result.responseObject);
      this.loanRepayment = result.responseObject;
    },
      err => {
        console.log(err);
      });
  }

}
