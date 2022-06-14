import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentAccountComponent } from './payment-account.component';
import { PaymentAccountListComponent } from './components/payment-account-list/payment-account-list.component';
import {
	PaymentAccountItemComponent
} from './components/payment-account-list/payment-account-item/payment-account-item.component';
import {
	PaymentAccountExpandedItemComponent
} from './components/payment-account-expanded-item/payment-account-expanded-item.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonControlsModule } from '../common-controls/common-controls.module';
import {
	PaymentAccountItemViewComponent
} from './components/payment-account-item-view/payment-account-item-view.component';


export const paymentAccountRoutes = [
	{ path : '*' , redirectTo : '' } ,
	{ path : '' , component : PaymentAccountListComponent , pathMatch : 'full' } ,
	{ path : 'create' , component : PaymentAccountItemViewComponent } ,
	{ path : ':id' , component : PaymentAccountItemViewComponent }
];

@NgModule ( {
	declarations : [
		PaymentAccountComponent ,
		PaymentAccountListComponent ,
		PaymentAccountItemComponent ,
		PaymentAccountExpandedItemComponent ,
		PaymentAccountItemViewComponent
	] , imports :  [
		CommonControlsModule , ReactiveFormsModule , CommonModule , RouterModule
	]
} )

export class PaymentAccountModule {}
