import { Component , OnInit } from '@angular/core';
import { CategoriesBaseComponent } from './components/categories-base/categories-base.component';
import { CategoryItemViewComponent } from './components/category-item-view/category-item-view.component';
import { CategoryCreateEditComponent } from './components/category-create-edit/category-create-edit.component';

export const categoriesRoutes = [
	{ path : '*' , redirectTo : '' } ,
	{ path : '' , component : CategoriesBaseComponent , pathMatch : 'full' } ,
	{ path : 'create' , component : CategoryCreateEditComponent } ,
	{ path : ':id' , component : CategoryItemViewComponent },
	{ path : 'edit/:id' , component : CategoryCreateEditComponent}
];

@Component ( {
	selector :    'app-categories-router' ,
	templateUrl : './categories-router.component.html' ,
	styleUrls :   [ './categories-router.component.scss' ]
} )
export class CategoriesRouterComponent implements OnInit {

	constructor () { }

	ngOnInit () : void {
	}

}
