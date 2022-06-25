import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS , HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PaymentAccountComponent } from './modules/payment-account/payment-account.component';
import { PaymentAccountModule , paymentAccountRoutes } from './modules/payment-account/payment-account.module';
import { ErrorNotificationInterceptor } from './modules/error-handler/error-notification.interceptor';
import { ErrorNotificationModule } from './modules/error-handler/error-notification.module';
import { AccountConfigModule , accountRoutes } from './modules/account/account-config.module';
import { AuthenticationInterceptor } from './modules/account/authentication.interceptor';
import { AccountConfigComponent } from './modules/account/account-config.component';
import { CategoriesRouterComponent , categoriesRoutes } from './modules/categories/categories-router.component';
import { CategoriesModule } from './modules/categories/categories.module';
import { TransactionsModule , transactionsRoute } from './modules/transactions/transactions.module';


let routes = [
	{ path : '*' , redirectTo : '' } ,
	{ path : '' , component : HomeComponent , pathMatch : 'full' } ,
	{ path : 'account' , component : AccountConfigComponent , children : accountRoutes } ,
	{ path : 'payment_accounts' , component : PaymentAccountComponent , children : paymentAccountRoutes } ,
	{ path : 'categories' , component : CategoriesRouterComponent , children : categoriesRoutes } ,
	transactionsRoute
];

@NgModule ( {
	declarations :  [
		AppComponent , NavMenuComponent , HomeComponent , CounterComponent , FetchDataComponent
	] , imports :   [
		TransactionsModule ,
		CategoriesModule ,
		ErrorNotificationModule ,
		AccountConfigModule ,
		PaymentAccountModule ,
		BrowserModule.withServerTransition ( { appId : 'ng-cli-universal' } ) ,
		HttpClientModule ,
		FormsModule ,
		RouterModule.forRoot ( routes )
	] , providers : [
		{ provide : HTTP_INTERCEPTORS , useClass : ErrorNotificationInterceptor , multi : true } ,
		{ provide : HTTP_INTERCEPTORS , useClass : AuthenticationInterceptor , multi : true }
	] , bootstrap : [ AppComponent ]
} )
export class AppModule {}
