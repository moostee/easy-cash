import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { GenericResponse } from '../interfaces/GenericResponse';
import { LoanRepayment } from '../interfaces/Loan';

@Injectable({
  providedIn: 'root'
})
export class LoanRepaymentService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getLoanRepaymentByLoanId(loanId?: number): Observable<GenericResponse<LoanRepayment[]>> {
    return this.http.get<GenericResponse<LoanRepayment[]>>(this.baseUrl + `LoanRepayment/${loanId}`);
  }
}
