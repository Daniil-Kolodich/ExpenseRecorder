import { Component , OnInit } from '@angular/core';
import { TransactionItem } from '../../models/transactions';
import { TransactionsService } from '../../services/transactions.service';

@Component ( {
	selector :    'transactions-list-base' ,
	templateUrl : './transactions-list-base.component.html' ,
	styleUrls :   [ './transactions-list-base.component.scss' ]
} )
export class TransactionsListBaseComponent implements OnInit {
	public transactions! : TransactionItem[];
	public isInitialized : boolean = false;

	constructor ( private transactionsService : TransactionsService ) { }

	ngOnInit () : void {
		this.transactionsService.getAll ().subscribe ( {
			next : ( data ) => this.transactions = data , complete : () => this.isInitialized = true
		} );
	}

}
