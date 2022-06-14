import { Component , Input , OnInit } from '@angular/core';
import { FormControl , FormGroup } from '@angular/forms';

@Component ( {
	selector : 'er-select' , templateUrl : './select.component.html' , styleUrls : [ './select.component.scss' ]
} )
export class SelectComponent implements OnInit {
	@Input () public items : any[] = [];
	@Input () public parentForm! : FormGroup;
	@Input () public controlName! : string;
	public control! : FormControl;

	constructor () { }

	ngOnInit () : void {
		this.control = this.parentForm.get ( this.controlName ) as FormControl;
	}

}
