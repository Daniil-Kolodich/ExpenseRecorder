import { Injectable } from '@angular/core';
import { HttpEvent , HttpHandler , HttpHeaders , HttpInterceptor , HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable ()
export class AuthenticationInterceptor implements HttpInterceptor {
	public intercept ( req : HttpRequest<any> , next : HttpHandler ) : Observable<HttpEvent<any>> {
		let request = req.clone ( { headers : this.getHeaders () } );
		return next.handle ( request );
	}

	private getHeaders () : HttpHeaders {
		return new HttpHeaders ( {
			'Authorization' : 'Bearer ' + localStorage.getItem ( 'token' )
		} );
	}
}
