import { Injectable } from '@angular/core';
import { HttpErrorResponse , HttpEvent , HttpHandler , HttpInterceptor , HttpRequest } from '@angular/common/http';
import { Observable , tap } from 'rxjs';
import { ErrorNotificationService } from './error-notification.service';

@Injectable ()
export class ErrorNotificationInterceptor implements HttpInterceptor {
	public constructor ( private errorNotificationService : ErrorNotificationService ) {
		console.log ( 'ErrorNotificationInterceptor created' );
	}

	public intercept ( req : HttpRequest<any> , next : HttpHandler ) : Observable<HttpEvent<any>> {
		return next.handle ( req ).pipe ( tap ( {
			error : ( error ) => this.handleError ( error )
		} ) );
	}

	private handleError ( error : HttpErrorResponse ) : void {
		if ( error.error instanceof ErrorEvent ) {
			console.error ( 'An error occurred:' , error.error.message );
		} else {
			console.error ( `Backend returned code ${ error.status }, ` + `body was: ${ error.error }` );
		}
		this.errorNotificationService.notify ( error );
	}
}
