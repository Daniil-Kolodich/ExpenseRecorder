import { Component , Input , OnInit } from '@angular/core';
import { CategoryItem } from '../../../../models/categories';
import { Router } from '@angular/router';

@Component ( {
	selector :    'categories-list-item' ,
	templateUrl : './categories-list-item.component.html' ,
	styleUrls :   [ './categories-list-item.component.scss' ]
} )
export class CategoriesListItemComponent implements OnInit {
	@Input () public category : CategoryItem | undefined = undefined;
	@Input() public isSimplifiedForm: boolean = false;
	constructor ( private router : Router ) { }

	ngOnInit () : void {
		console.log ( this.category );
	}
}
