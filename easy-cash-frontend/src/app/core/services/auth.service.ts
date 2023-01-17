import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GenericResponse } from '../interfaces/GenericResponse';
import { AuthResponse } from '../interfaces/User';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<GenericResponse<AuthResponse>> {
    return this.http.post<GenericResponse<AuthResponse>>(this.baseUrl + 'User/login', {
      email: email,
      password: password
    });
  }

}