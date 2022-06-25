import { Component , Input , OnInit } from '@angular/core';
import { TransactionItem } from '../../models/transactions';

@Component({
	selector: 'transactions-list',
	templateUrl: './transactions-list.component.html',
	styleUrls: ['./transactions-list.component.scss']
})
export class TransactionsListComponent implements OnInit {
	@Input() transactions!: TransactionItem[];
	constructor() { }

	ngOnInit(): void {
	}

}
