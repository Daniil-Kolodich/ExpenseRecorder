import { Inject , Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SignIn , SignInResponse , SignUp , SignUpResponse } from './models/account';

@Injectable ( {
	providedIn : 'root'
} )
export class AuthenticationService {
	constructor ( private http : HttpClient , @Inject ( 'BASE_URL' ) private baseUrl : string ) {}

	public signIn ( model : SignIn ) : Observable<SignInResponse> {
		return this.http.post<SignInResponse> ( this.baseUrl + 'User/login' , model );
	}


	public signUp(model : SignUp) : Observable<SignUpResponse> {
		return this.http.post<SignUpResponse>(this.baseUrl + 'User/register', model);
	}
	public get isAuthenticated () : boolean {
		return localStorage.getItem ( 'token' ) !== null;
	}

	public get User() : string {
		return localStorage.getItem ( 'userName' ) ?? '';
	}

	public logout() : void {
		localStorage.removeItem ( 'token' );
		localStorage.removeItem ( 'userName' );
	}
}
