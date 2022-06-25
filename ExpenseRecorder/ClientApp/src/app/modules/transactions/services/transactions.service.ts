import { Inject , Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TransactionCreateUpdateItem , TransactionItem } from '../models/transactions';

@Injectable ( {
	providedIn : 'root'
} )
export class TransactionsService {
	public url : string = 'Transaction';
	public url_ext : string = this.url + '/';

	constructor ( private http : HttpClient , @Inject ( 'BASE_URL' ) private baseUrl : string ) {}

	// TODO: add search query ? also made that api endpoint support search queries
	public getAll () : Observable<TransactionItem[]> {
		return this.http.get<TransactionItem[]> ( this.baseUrl + this.url_ext + 'GetAll' );
	}

	public get ( id : number ) : Observable<TransactionItem> {
		return this.http.get<TransactionItem> ( this.baseUrl + this.url_ext + id );
	}

	public create ( transaction : TransactionCreateUpdateItem ) : Observable<TransactionItem> {
		return this.http.post<TransactionItem> ( this.baseUrl + this.url , transaction );
	}

	public update ( transaction : TransactionCreateUpdateItem , id : number ) : Observable<TransactionItem> {
		return this.http.put<TransactionItem> ( this.baseUrl + this.url_ext + id , transaction );
	}

	public delete ( id : number ) : Observable<TransactionItem> {
		return this.http.delete<TransactionItem> ( this.baseUrl + this.url_ext + id );
	}
}
