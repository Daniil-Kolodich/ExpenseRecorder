import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesRouterComponent } from './categories-router.component';
import { CategoriesBaseComponent } from './components/categories-base/categories-base.component';
import { CategoriesListComponent } from './components/categories-base/categories-list/categories-list.component';
import {
	CategoriesListItemComponent
} from './components/categories-base/categories-list/categories-list-item/categories-list-item.component';
import { RouterModule } from '@angular/router';
import { CategoryItemViewComponent } from './components/category-item-view/category-item-view.component';
import { CommonControlsModule } from '../common-controls/common-controls.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CategoryCreateEditComponent } from './components/category-create-edit/category-create-edit.component';


@NgModule ( {
	declarations : [
		CategoriesRouterComponent ,
		CategoriesBaseComponent ,
		CategoriesListComponent ,
		CategoriesListItemComponent ,
		CategoryItemViewComponent ,
		CategoryCreateEditComponent
	] , imports :  [
		CommonControlsModule , ReactiveFormsModule , CommonModule , RouterModule
	]
} )
export class CategoriesModule {}
