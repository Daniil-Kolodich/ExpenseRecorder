import { Inject , Injectable } from '@angular/core';
import { HttpClient , HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
	PaymentAccountCreateUpdateItem , PaymentAccountItem , PaymentAccountViewItem
} from './models/payment-account';

@Injectable ( {
	providedIn : 'root'
} )
export class PaymentAccountService {
	constructor ( private http : HttpClient , @Inject ( 'BASE_URL' ) private baseUrl : string ) {}

	public getAll () : Observable<PaymentAccountItem[]> {
		return this.http.get<PaymentAccountItem[]> ( this.baseUrl + 'PaymentAccount/GetAll'  );
	}

	public get ( id : number ) : Observable<PaymentAccountViewItem> {
		return this.http.get<PaymentAccountViewItem> ( this.baseUrl + 'PaymentAccount/' + id );
	}

	public create ( paymentAccount : PaymentAccountCreateUpdateItem ) : Observable<PaymentAccountViewItem> {
		return this.http.post<PaymentAccountViewItem> ( this.baseUrl + 'PaymentAccount' , paymentAccount);
	}

	public update ( paymentAccount : PaymentAccountCreateUpdateItem ,
		id : number ) : Observable<PaymentAccountViewItem> {
		return this.http.put<PaymentAccountViewItem> ( this.baseUrl + 'PaymentAccount/' + id , paymentAccount );
	}

	public delete ( id : number ) : Observable<PaymentAccountItem> {
		return this.http.delete<PaymentAccountItem> ( this.baseUrl + 'PaymentAccount/' + id );
	}
}
