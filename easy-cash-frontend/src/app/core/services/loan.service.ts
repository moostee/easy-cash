import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { GenericResponse } from '../interfaces/GenericResponse';
import { CreateLoan, Loans } from '../interfaces/Loan';

@Injectable({
  providedIn: 'root'
})
export class LoanService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  addNewLoan(loan: CreateLoan): Observable<GenericResponse<string>> {
    return this.http.post<GenericResponse<string>>(this.baseUrl + 'Loan', loan);
  }

  getAllLoans(): Observable<GenericResponse<Loans[]>> {
    return this.http.get<GenericResponse<Loans[]>>(this.baseUrl + 'Loan');
  }

  getLoanByUserId(userId : number): Observable<GenericResponse<Loans[]>> {
    return this.http.get<GenericResponse<Loans[]>>(this.baseUrl + `Loan/${userId}`);
  }

  repayLoan(userId: number): Observable<GenericResponse<string>> {
    return this.http.put<GenericResponse<string>>(this.baseUrl + 'Loan/repay', {
      "userId" : userId
    });
  }

}
