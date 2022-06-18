import { Component , OnInit } from '@angular/core';
import { AuthenticationService } from '../../authentication.service';
import { SignIn } from '../../models/account';
import { Location } from '@angular/common';
import { FormControl , FormGroup , Validators } from '@angular/forms';

@Component ( {
	selector : 'app-sign-in' , templateUrl : './sign-in.component.html' , styleUrls : [ './sign-in.component.scss' ]
} )
export class SignInComponent implements OnInit {
	public form! : FormGroup;

	constructor ( private authenticationService : AuthenticationService , private location : Location ) {
		this.initForm ();
	}

	ngOnInit () : void {
	}

	public signIn () {
		this.form!.markAllAsTouched ();
		if ( this.form!.invalid ) {
			return;
		}
		let user = this.form.value as SignIn;
		let loginResponse = this.authenticationService.signIn ( user );

		loginResponse.subscribe ( result => {
			localStorage.setItem ( 'token' , result.token );
			localStorage.setItem ( 'userName' , result.userName );
			this.location.back ();
		} , error => console.error ( error ) );
	}

	private initForm () {
		this.form = new FormGroup ( {
			userName : new FormControl ( '' , Validators.required ) ,
			password : new FormControl ( '' , Validators.required )
		} );
	}
}