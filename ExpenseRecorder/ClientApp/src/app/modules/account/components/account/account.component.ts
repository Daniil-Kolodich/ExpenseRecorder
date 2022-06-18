import { Component , OnInit } from '@angular/core';
import { AuthenticationService } from '../../authentication.service';
import { ActivatedRoute , Router } from '@angular/router';

@Component ( {
	selector : 'account' , templateUrl : './account.component.html' , styleUrls : [ './account.component.scss' ]
} )
export class AccountComponent implements OnInit {
	constructor ( private authenticationService : AuthenticationService, private router: Router, private route: ActivatedRoute ) { }

	public get isAuthenticated () : boolean {return this.authenticationService.isAuthenticated;}

	public get User () : string {return this.authenticationService.User;}

	ngOnInit () : void {
	}

	public onLogout() : void {
		this.authenticationService.logout();
	}

	public onSignIn() : void {
		this.router.navigate(['account/sign_in']);
	}

	public onSignUp() : void {
		this.router.navigate(['account/sign_up']);
	}
}
