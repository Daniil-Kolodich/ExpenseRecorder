import { Component , OnInit } from '@angular/core';
import { PaymentAccountService } from '../../../../services/payment-account.service';
import { Router } from '@angular/router';
import { PaymentAccountItem } from '../../models/paymentAccountModels';

@Component ( {
	selector :    'payment-account-list' ,
	templateUrl : './payment-account-list.component.html' ,
	styleUrls :   [ './payment-account-list.component.scss' ]
} )
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


