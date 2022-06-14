import { Component , Input , OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component ( {
	selector :    'er-control-validation' ,
	templateUrl : './control-validation.component.html' ,
	styleUrls :   [ './control-validation.component.scss' ]
} )
export class ControlValidationComponent implements OnInit {
	@Input () public control! : FormControl;

	constructor () { }

	ngOnInit () : void {
	}

}
