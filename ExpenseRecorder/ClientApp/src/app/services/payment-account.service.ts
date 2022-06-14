import { Inject , Injectable } from '@angular/core';
import { HttpClient , HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
	PaymentAccountCreateUpdateItem , PaymentAccountItem , PaymentAccountViewItem
} from '../modules/payment-account/models/paymentAccountModels';

@Injectable ( {
	providedIn : 'root'
} )
export class PaymentAccountService {

	constructor ( private http : HttpClient , @Inject ( 'BASE_URL' ) private baseUrl : string ) {}

	public getAll () : Observable<PaymentAccountItem[]> {
		return this.http.get<PaymentAccountItem[]> ( this.baseUrl + 'PaymentAccount/GetAll' ,
			{ headers : this.getHeaders () } );
	}

	public get ( id : number ) : Observable<PaymentAccountViewItem> {
		return this.http.get<PaymentAccountViewItem> ( this.baseUrl + 'PaymentAccount/' + id ,
			{ headers : this.getHeaders () } );
	}

	public create ( paymentAccount : PaymentAccountCreateUpdateItem ) : Observable<PaymentAccountViewItem> {
		return this.http.post<PaymentAccountViewItem> ( this.baseUrl + 'PaymentAccount' , paymentAccount ,
			{ headers : this.getHeaders () } );
	}

	public update ( paymentAccount : PaymentAccountCreateUpdateItem ,
		id : number ) : Observable<PaymentAccountViewItem> {
		return this.http.put<PaymentAccountViewItem> ( this.baseUrl + 'PaymentAccount/' + id , paymentAccount ,
			{ headers : this.getHeaders () } );
	}

	public delete ( id : number ) : Observable<PaymentAccountItem> {
		return this.http.delete<PaymentAccountItem> ( this.baseUrl + 'PaymentAccount/' + id ,
			{ headers : this.getHeaders () } );
	}

	private getHeaders () : HttpHeaders {
		return new HttpHeaders ( {
			'Authorization' : 'Bearer ' + localStorage.getItem ( 'token' )
		} );
	}
}
