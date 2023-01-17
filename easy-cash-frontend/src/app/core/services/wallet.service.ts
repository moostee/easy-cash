import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { GenericResponse } from '../interfaces/GenericResponse';
import { Wallet } from '../interfaces/Loan';

@Injectable({
  providedIn: 'root'
})
export class WalletService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getWalletByUserId(userId : number): Observable<GenericResponse<Wallet>> {
    return this.http.get<GenericResponse<Wallet>>(this.baseUrl + `Wallet/${userId}`);
  }
  
}
