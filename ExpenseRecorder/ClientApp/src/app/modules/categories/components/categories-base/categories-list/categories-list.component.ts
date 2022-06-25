import { Component , EventEmitter , Input , Output } from '@angular/core';
import { CategoryItem } from '../../../models/categories';

@Component ( {
	selector :    'categories-list' ,
	templateUrl : './categories-list.component.html' ,
	styleUrls :   [ './categories-list.component.scss' ]
} )
export class CategoriesListComponent {
	@Input () public categories : CategoryItem[] | undefined = undefined;
	@Output () public itemClick : EventEmitter<number> = new EventEmitter<number> ();
	@Input () public isSimplifiedForm : boolean = false;

	constructor () { }

	public onItemClick ( category : CategoryItem ) {
		this.itemClick.emit ( category.id );
	}
}
