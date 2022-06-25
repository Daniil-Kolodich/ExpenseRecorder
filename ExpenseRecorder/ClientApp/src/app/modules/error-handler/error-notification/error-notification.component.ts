import { Component , OnInit } from '@angular/core';
import { ErrorNotificationService } from '../error-notification.service';

@Component ( {
	selector :    'error-notification' ,
	templateUrl : './error-notification.component.html' ,
	styleUrls :   [ './error-notification.component.scss' ]
} )
export class ErrorNotificationComponent implements OnInit {
	public isNotificationVisible : boolean = false;
	public errorMessage: string = '';

	constructor ( private errorNoticationService : ErrorNotificationService ) {
		this.errorNoticationService.notification.subscribe ( {
			next : ( value ) => {
				console.log ( 'Error Notif Component get ' , value );
				this.errorMessage = value!;
				this.isNotificationVisible = true;
				setTimeout ( () => {this.isNotificationVisible = false;} , 5000 );
			}
		} );
	}

	ngOnInit () : void {
		this.isNotificationVisible = false;
	}

}
