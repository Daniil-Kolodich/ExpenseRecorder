import { Component , OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component ( {
	selector : 'app-sign-in' , templateUrl : './sign-in.component.html' , styleUrls : [ './sign-in.component.scss' ]
} )
export class SignInComponent implements OnInit {
	public signInRequest = {
		userName : '' , password : ''
	} as SignInRequest;

	constructor ( private authenticationService : AuthenticationService , private router : Router ) { }

	ngOnInit () : void {
	}

	public signIn () {
		let loginResponse = this.authenticationService.signIn ( this.signInRequest );
		loginResponse.subscribe ( result => {
			localStorage.setItem ( 'token' , result.token );
			localStorage.setItem ( 'userName' , result.userName );
			this.router.navigate ( [ '/' ] );
		} , error => console.error ( error ) );
	}
}

export interface SignInRequest {
	userName : string;
	password : string;
}
