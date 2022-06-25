import { Component , OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl , FormGroup , Validators } from '@angular/forms';
import { CategoriesService } from '../../categories.service';
import { CategoryCreateUpdateItem , CategoryItem } from '../../models/categories';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';

@Component ( {
	selector :    'app-category-create-edit' ,
	templateUrl : './category-create-edit.component.html' ,
	styleUrls :   [ './category-create-edit.component.scss' ]
} )
export class CategoryCreateEditComponent implements OnInit {
	public id : number | undefined = undefined;
	public form : FormGroup | undefined = undefined;

	constructor ( private route : ActivatedRoute , private categoriesService : CategoriesService ,
		private location : Location ) {
		this.handleRouteParams ();
	}

	public get isFormInitialized () : boolean {
		return !!this.form;
	}

	public get isCreating () : boolean {
		return !this.id;
	}

	public onSubmit () {
		this.form!.markAllAsTouched ();
		if ( this.form!.invalid ) {
			return;
		}

		let category = this.form!.value as CategoryCreateUpdateItem;
		let promise : Observable<CategoryItem>;
		if ( this.isCreating ) {

			promise = this.categoriesService.create ( category );
		} else {
			promise = this.categoriesService.update ( category , this.id! );
		}
		promise.subscribe ( {
			error : ( e ) => console.log ( 'something gone wrong' , e ) , complete : () => this.location.back ()
		} );
	}

	ngOnInit () : void {
	}

	private handleRouteParams () {
		let routeId : string | null = this.route.snapshot.paramMap.get ( 'id' );
		if ( routeId ) {
			this.id = parseInt ( routeId , 10 );
			this.getCategory ();
		} else {
			this.initForm ();
		}
	}

	private getCategory () {
		let promise = this.categoriesService.get ( this.id! );
		let category : CategoryItem;
		promise.subscribe ( {
			next : ( data ) => category = data , complete : () => this.initForm ( category )
		} );
	}

	private initForm ( category : CategoryItem | undefined = undefined ) {
		console.log ( 'init form called with' , category );
		this.form = new FormGroup ( {
			name :  new FormControl ( category?.name ?? '' , [ Validators.required ] ) ,
			color : new FormControl ( category?.color ?? 'red' , [ Validators.required ] ) ,
			icon :  new FormControl ( category?.icon ?? '1.svg' , [ Validators.required ] )
		} );
	}
}
