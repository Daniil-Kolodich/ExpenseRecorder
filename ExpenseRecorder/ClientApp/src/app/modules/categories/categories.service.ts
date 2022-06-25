import { Inject , Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  PaymentAccountCreateUpdateItem ,
  PaymentAccountItem ,
  PaymentAccountViewItem
} from '../payment-account/models/payment-account';
import { CategoryCreateUpdateItem , CategoryItem } from './models/categories';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  public url : string = 'Category';
  public url_ext : string = this.url + '/';
  constructor ( private http : HttpClient , @Inject ( 'BASE_URL' ) private baseUrl : string ) {}

  // TODO: add search query ? also made that api endpoint support search queries
  public getAll () : Observable<CategoryItem[]> {
    return this.http.get<CategoryItem[]> ( this.baseUrl + this.url_ext + 'GetAll'  );
  }

  public get ( id : number ) : Observable<CategoryItem> {
    return this.http.get<CategoryItem> ( this.baseUrl + this.url_ext + id );
  }

  public create ( category : CategoryCreateUpdateItem ) : Observable<CategoryItem> {
    return this.http.post<CategoryItem> ( this.baseUrl + this.url , category);
  }

  public update ( category : CategoryCreateUpdateItem ,
      id : number ) : Observable<CategoryItem> {
    return this.http.put<CategoryItem> ( this.baseUrl + this.url_ext + id , category );
  }

  public delete ( id : number ) : Observable<PaymentAccountItem> {
    return this.http.delete<PaymentAccountItem> ( this.baseUrl + 'PaymentAccount/' + id );
  }
}
