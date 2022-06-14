import { Component , OnInit } from '@angular/core';
import { PaymentAccountCreateUpdateItem , PaymentAccountViewItem } from '../../models/paymentAccountModels';
import { PaymentAccountService } from '../../../../services/payment-account.service';
import { ActivatedRoute , Router } from '@angular/router';
import { Location } from '@angular/common';
import { FormControl , FormGroup , Validators } from '@angular/forms';

@Component ( {
	selector :    'payment-account-item-view' ,
	templateUrl : './payment-account-item-view.component.html' ,
	styleUrls :   [ './payment-account-item-view.component.scss' ]
} )
export class PaymentAccountItemViewComponent implements OnInit {
	public paymentAccount : PaymentAccountViewItem | null = null;
	public id : number | undefined = undefined;
	public isEditing : boolean = false;
	public isCreating : boolean = false;
	public form : FormGroup | undefined = undefined;

	constructor ( private paymentAccountService : PaymentAccountService , private router : Router ,
		private route : ActivatedRoute , private location : Location ) {
		this.initFields ();
	}

	public isFormVisible () : boolean {return this.isEditing || this.isCreating;}

	// TODO Make this as getters
	// TODO : add check if transactions list is actually empty
	public isTransactionsVisible () : boolean { return !this.isFormVisible () && this.paymentAccount !== null; }

	public isDeletable () : boolean { return !this.isFormVisible () && this.paymentAccount !== null; }

	public isEditable () : boolean { return this.paymentAccount !== null; }

	public onBack () {
		this.location.back ();
	}

	public onDelete () {

	}

	public availableCurrencies () : string[] {
		return [ 'USD' , 'EUR' , 'RUB' , 'BYN' ];
	}

	public onEdit () {
		this.isEditing = !this.isEditing;
	}

	public onSubmit () : void {
		this.form!.markAllAsTouched ();
		if ( this.form!.invalid ) {
			return;
		}
		let payload = this.form!.value as PaymentAccountCreateUpdateItem;
		console.log ( 'sending request' , payload );
		if ( this.id === undefined ) {
			this.paymentAccountService.create ( payload )
				.subscribe ( {
					next :     ( model : PaymentAccountViewItem ) => this.resetComponent ( model ) ,
					error :    ( e ) => console.error ( e ) ,
					complete : () => console.log ( 'success on creating' )
				} );
		} else {
			this.paymentAccountService.update ( payload , this.id! )
				.subscribe ( {
					next :     ( model : PaymentAccountViewItem ) => this.resetComponent ( model ) ,
					error :    ( e ) => console.error ( e ) ,
					complete : () => console.log ( 'success on updating' )
				} );
		}
	}

	ngOnInit () : void {
	}


	private resetComponent ( response : PaymentAccountViewItem ) {
		if ( this.paymentAccount === null ) {
			this.paymentAccount = response;
		} else {
			this.paymentAccount!.name = response.name;
			this.paymentAccount!.currency = response.currency;
			this.paymentAccount!.balance = response.balance;
		}
		this.id = response.id;
		this.isEditing = false;
		this.isCreating = false;
	}

	private initFields () : void {
		let routeId = this.route.snapshot.paramMap.get ( 'id' );
		if ( routeId !== null ) {
			this.id = parseInt ( routeId );
			this.paymentAccountService.get ( this.id ).subscribe ( {
				next :     ( model : PaymentAccountViewItem ) => this.paymentAccount = model ,
				error :    ( e ) => console.error ( e ) ,
				complete : () => this.initForm ()
			} );
		} else {
			this.isCreating = true;
			this.initForm ();
		}
	}

	private initForm () : void {
		this.form = new FormGroup ( {
			name :     new FormControl ( this.paymentAccount?.name ?? '' , [ Validators.required ] ) ,
			currency : new FormControl ( this.paymentAccount?.currency ?? 'USD' , [ Validators.required ] ) ,
			balance :  new FormControl ( this.paymentAccount?.balance ?? 0 ,
				[ Validators.required , Validators.pattern ( /^[0-9]\d*$/ ) ] )
		} );

	}

}
