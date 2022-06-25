import { Component , OnInit } from '@angular/core';
import { PaymentAccountService } from '../../payment-account.service';
import { Router } from '@angular/router';
import { PaymentAccountItem } from '../../models/payment-account';

@Component ( {
	selector :    'payment-account-list' ,
	templateUrl : './payment-account-list.component.html' ,
	styleUrls :   [ './payment-account-list.component.scss' ]
} )
// TODO : make some shell component for work with service and pass all the data to actual list component
export class PaymentAccountListComponent implements OnInit {
	public paymentAccounts : PaymentAccountItem[] | undefined = undefined;

	constructor ( private paymentAccountService : PaymentAccountService , private router : Router ) {
		let promise = this.paymentAccountService.getAll ();
		promise.subscribe ( data => {this.paymentAccounts = data;} , error => {console.log ( error );} );
	}

	public add () : void {
		this.router.navigate ( [ 'payment_accounts/create' ] );
	}

	ngOnInit () : void {
	}

}


