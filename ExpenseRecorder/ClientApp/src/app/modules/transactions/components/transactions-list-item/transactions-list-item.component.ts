import { Component , Input , OnInit } from '@angular/core';
import { TransactionItem } from '../../models/transactions';

@Component({
	selector: 'transactions-list-item',
	templateUrl: './transactions-list-item.component.html',
	styleUrls: ['./transactions-list-item.component.scss']
})
export class TransactionsListItemComponent implements OnInit {
	@Input() transaction!: TransactionItem;
	constructor() { }

	ngOnInit(): void {
	}

}
