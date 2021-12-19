import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { HttpMethodType } from '../utils/general.enums';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, map ,share} from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
    apiUrl: string;
    httpOptions={
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        }),
        params: new HttpParams({})
    }

    constructor(private http: HttpClient, private router: Router){
        this.apiUrl = "https://localhost:44307/";
    }

    public GetData(){
        var apiUrl = this.apiUrl + 'Contacts/GetList';
        return this.sendApiRequest(HttpMethodType.POST, null, this.httpOptions, apiUrl);
    }

    sendApiRequest(method: HttpMethodType, data: any, options: any, serverUrl: string): Observable<any> 
    {
        debugger
        if(method == HttpMethodType.GET){
            return this.http.get(serverUrl, {params: data}).pipe(
                map(data => {
                    return data;
                }), (catchError(this.handleError))
            )
        }
        else
      {
          return this.http.post(serverUrl,data,options).pipe(
            map((data:any)=> {
              return data;
            }), (catchError(this.handleError))
          )

      }
    }

    handleError(error: HttpErrorResponse){
        debugger
        if(error instanceof HttpErrorResponse){
            if(error.error instanceof ErrorEvent) {
                console.log("Error Event");
            }
            else{
                console.log(`error status : ${error.status} ${error.statusText}`);
                this.router.navigateByUrl("/home");
            }
        }
        else{
            console.error("some thing else happened");
        }
        return throwError(error);
    }
}