import { Component , OnInit } from '@angular/core';
import { AuthenticationService } from '../../authentication.service';
import { FormControl , FormGroup , Validators } from '@angular/forms';
import { SignIn , SignUp } from '../../models/account';
import { Location } from '@angular/common';

@Component ( {
	selector : 'app-sign-up' , templateUrl : './sign-up.component.html' , styleUrls : [ './sign-up.component.scss' ]
} )
export class SignUpComponent implements OnInit {
	public form!: FormGroup;
	constructor (private authenticationService: AuthenticationService, private location: Location) {
		this.initForm ();
	}

	ngOnInit () : void {
	}

	private initForm   () {
		this.form = new FormGroup ({
			userName : new FormControl ( '' , Validators.required ) ,
			email : new FormControl ( '' , Validators.required ),
			password : new FormControl ( '' , Validators.required ),
			// TODO : add validator for confirm password
			confirmPassword : new FormControl ( '' , Validators.required ),
		});
	}

	public onSignUp () {
		this.form!.markAllAsTouched ();
		if ( this.form!.invalid ) {
			return;
		}
		let user = this.form.value as SignUp;
		let signUpResponse = this.authenticationService.signUp ( user );

		signUpResponse.subscribe ( result => {
			this.onSuccess();
		} , error => console.error ( error ) );
	}

	private onSuccess () {
		let user = this.form.value as SignIn;

		let loginResponse = this.authenticationService.signIn ( user );

		loginResponse.subscribe ( result => {
			localStorage.setItem ( 'token' , result.token );
			localStorage.setItem ( 'userName' , result.userName );
			this.location.back ();
		} , error => console.error ( error ) );
	}
}
