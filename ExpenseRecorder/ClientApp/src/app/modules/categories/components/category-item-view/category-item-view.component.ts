import { Component , OnInit } from '@angular/core';
import { CategoryItem } from '../../models/categories';
import { CategoriesService } from '../../categories.service';
import { ActivatedRoute , Router } from '@angular/router';
import { Location } from '@angular/common';

@Component ( {
	selector :    'app-category-item-view' ,
	templateUrl : './category-item-view.component.html' ,
	styleUrls :   [ './category-item-view.component.scss' ]
} )
export class CategoryItemViewComponent implements OnInit {
	public category : CategoryItem | undefined = undefined;
	public id : number | undefined = undefined;

	constructor ( private categoriesService : CategoriesService , private router : Router ,
		private location : Location , private route : ActivatedRoute ) {
		this.initFields ();
	}

	ngOnInit () : void {
	}

	public onBack () {
		this.location.back ();
	}

	public onEdit () {
		this.router.navigate ( [ 'categories','edit' , this.category!.id ] );
	}

	private initFields () : void {
		let routeId = this.route.snapshot.paramMap.get ( 'id' );
		if ( routeId !== null ) {
			this.id = parseInt ( routeId );
			this.categoriesService.get ( this.id ).subscribe ( {
				next : ( model : CategoryItem ) => this.category = model , error : () => this.location.back ()
			} );
		} else {
			console.log ( 'not able to found such id' );
			this.location.back ();
		}
	}
}
