import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GenericResponse } from '../interfaces/GenericResponse';
import { ActivateOrDeActivateUser, CreateUser, User } from '../interfaces/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  addNewUser(user: CreateUser): Observable<GenericResponse<string>> {
    return this.http.post<GenericResponse<string>>(this.baseUrl + 'User', user);
  }

  getAllUser(): Observable<GenericResponse<User[]>> {
    return this.http.get<GenericResponse<User[]>>(this.baseUrl + 'User');
  }

  activateOrDeactivateUser(user : ActivateOrDeActivateUser): Observable<GenericResponse<string>> {
    return this.http.put<GenericResponse<string>>(this.baseUrl + 'User/activate-deactivate', user);
  }

}
