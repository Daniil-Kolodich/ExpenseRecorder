export interface PaymentAccountItem {
	id : number;
	name : string;
	balance : number;
	currency : string;
}

export interface PaymentAccountViewItem {
	id : number;
	name : string;
	balance : number;
	currency : string;
	transactions : string[];
}

export interface PaymentAccountCreateUpdateItem {
	name : string;
	balance : number;
	currency : string;
}

