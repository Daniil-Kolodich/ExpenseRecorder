import { Component , OnInit } from '@angular/core';
import { CategoriesService } from '../../categories.service';
import { CategoryItem } from '../../models/categories';
import { Router } from '@angular/router';

@Component ( {
	selector :    'categories-base' ,
	templateUrl : './categories-base.component.html' ,
	styleUrls :   [ './categories-base.component.scss' ]
} )
export class CategoriesBaseComponent implements OnInit {
	public categories! : CategoryItem[];
	public isInitialized : boolean = false;

	constructor ( private categoriesService : CategoriesService , private router : Router ) {
	}

	public onAdd () {
		this.router.navigate ( [ 'categories' , 'create' ] );
	}

	public onView ( categoryId : number ) {
		this.router.navigate ( [ 'categories' , categoryId ] );
	}

	public ngOnInit () : void {
		let promise = this.categoriesService.getAll ();
		promise.subscribe ( {
			next :     data => {this.categories = data;} ,
			error :    error => {console.log ( error );} ,
			complete : () => this.isInitialized = true
		} );
	}
}
