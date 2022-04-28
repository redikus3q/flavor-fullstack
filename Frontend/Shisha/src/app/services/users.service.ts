import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  public url = 'https://localhost:44361/api/user';
  constructor(
    private http: HttpClient
  ) { }

  public getUserById(id: any): Observable<any>{
    return this.http.get<any>(`${this.url}/${id}`);
  }
}
