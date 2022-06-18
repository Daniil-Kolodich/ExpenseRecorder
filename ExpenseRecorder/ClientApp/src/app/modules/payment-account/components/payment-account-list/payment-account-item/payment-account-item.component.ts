import { Component , Input , OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PaymentAccountItem } from '../../../models/payment-account';

@Component ( {
	selector :    'payment-account-item' ,
	templateUrl : './payment-account-item.component.html' ,
	styleUrls :   [ './payment-account-item.component.scss' ]
} )
export class PaymentAccountItemComponent implements OnInit {
	@Input () public paymentAccount! : PaymentAccountItem;

	constructor ( private router : Router ) { }

	ngOnInit () : void {
	}

	public view () {
		this.router.navigate ( [ 'payment_accounts' , this.paymentAccount.id ] );
	}
}
