import { Injectable } from '@angular/core';
import { BehaviorSubject , Observable } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable ( {
	providedIn : 'root'
} )
export class ErrorNotificationService {
	public notification : Observable<string | null>;
	private errorSubject = new BehaviorSubject<string | null> ( null );

	constructor () {
		this.notification = this.errorSubject.asObservable ();
	}

	public notify ( error : HttpErrorResponse ) : void {
		// do some stuff with the error message displayed to the user

		console.log('service get : ',error);
		let message = error.error?.message ?? error.message;
		this.errorSubject.next ( message );
	}
}
