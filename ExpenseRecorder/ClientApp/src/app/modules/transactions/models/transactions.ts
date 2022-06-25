// public int              Id          { get ; set ; }
// public string?          Description { get ; set ; }
// public decimal          Amount      { get ; set ; }
// public int              CategoryId  { get ; set ; }
// public CategoryResponse Category    { get ; set ; } = null! ;
// public DateTime         Date        { get ; set ; }
// public string           Type        { get ; set ; } = String.Empty ;
// public int PaymentAccountId { get; set; }
// public PaymentAccountResponse PaymentAccount { get; set; } = null!;

import { CategoryItem } from '../../categories/models/categories';
import { PaymentAccountItem } from '../../payment-account/models/payment-account';

export interface TransactionItem {
	id : number;
	description : string | null;
	amount : number;
	date : Date;
	type : string;
	category : CategoryItem;
	paymentAccount : PaymentAccountItem;
}

export interface TransactionCreateUpdateItem {
	description : string | null;
	amount : number;
	categoryId : number;
	paymentAccountId : number;
	date : Date;
	type : string;
}