import { Component , HostBinding , Input , OnInit } from '@angular/core';
import { FormControl , FormGroup } from '@angular/forms';

@Component ( {
	selector : 'er-input' , templateUrl : './input.component.html' , styleUrls : [ './input.component.scss' ]
} )
export class InputComponent implements OnInit {
	@Input () public parentForm! : FormGroup;
	@Input () public controlName! : string;
	@Input () public placeholder : string = '';
	@Input() public type: string = 'text';
	public control! : FormControl;
	@HostBinding ( 'class' ) @Input () public class = 'col-12 er-input';

	constructor () {
	}


	ngOnInit () : void {
		this.control = this.parentForm.get ( this.controlName ) as FormControl;
	}

}
