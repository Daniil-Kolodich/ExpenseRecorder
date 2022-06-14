import { Inject , Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignInRequest } from '../sign-in/sign-in.component';
import { Observable } from 'rxjs';

@Injectable ( {
	providedIn : 'root'
} )
export class AuthenticationService {
	constructor ( private http : HttpClient , @Inject ( 'BASE_URL' ) private baseUrl : string ) {}

	public signIn ( model : SignInRequest ) : Observable<LoginResponse> {
		return this.http.post<LoginResponse> ( this.baseUrl + 'User/login' , model );
	}
}


interface LoginResponse {
	userName : string;
	token : string;
}
