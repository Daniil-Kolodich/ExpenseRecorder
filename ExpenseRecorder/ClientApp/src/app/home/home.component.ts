import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component ( {
	selector : 'app-home' , templateUrl : './home.component.html'
} )
export class HomeComponent {
	constructor ( private router : Router ) {}

	public get IsAuthenticated () : boolean {
		return localStorage.getItem ( 'token' ) !== null;
	}

	public get UserName () : string {
		return localStorage.getItem ( 'userName' ) ?? '';
	}

	public signIn () {
		this.router.navigate ( [ '/sign_in' ] );
	}

	public signUp () {
		this.router.navigate ( [ '/sign_up' ] );
	}

	public signOut () {
		localStorage.removeItem ( 'token' );
		localStorage.removeItem ( 'userName' );
	}
}
