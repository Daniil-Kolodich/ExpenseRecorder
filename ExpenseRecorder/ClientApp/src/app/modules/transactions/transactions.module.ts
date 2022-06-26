import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionsRouterComponent } from './components/transactions-router/transactions-router.component';
import { TransactionsListComponent } from './components/transactions-list/transactions-list.component';
import { TransactionsListItemComponent } from './components/transactions-list-item/transactions-list-item.component';
import { TransactionItemViewComponent } from './components/transaction-item-view/transaction-item-view.component';
import { TransactionCreateEditComponent } from './components/transaction-create-edit/transaction-create-edit.component';
import { RouterModule } from '@angular/router';
import { TransactionsListBaseComponent } from './components/transactions-list-base/transactions-list-base.component';
import { CategoriesModule } from '../categories/categories.module';

const transactionsChildRoutes = [
	{ path : '*' , redirectTo : '' },
	{ path : '' , component : TransactionsListBaseComponent , pathMatch : 'full' } ,
	// { path : 'create' , component : CategoryCreateEditComponent } ,
	// { path : ':id' , component : CategoryItemViewComponent },
	// { path : 'edit/:id' , component : CategoryCreateEditComponent}
];

export const transactionsRoute = {
	path :      'transactions' ,
	component : TransactionsRouterComponent ,
	children :  transactionsChildRoutes
};

@NgModule ( {
	declarations : [
		TransactionsRouterComponent ,
		TransactionsListComponent ,
		TransactionsListItemComponent ,
		TransactionItemViewComponent ,
		TransactionCreateEditComponent ,
		TransactionsListBaseComponent
	] , imports : [
		CommonModule , RouterModule , CategoriesModule
	]
} )
export class TransactionsModule {}
