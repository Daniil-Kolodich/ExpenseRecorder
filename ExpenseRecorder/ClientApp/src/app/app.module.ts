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
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { PaymentAccountComponent } from './modules/payment-account/payment-account.component';
import { PaymentAccountModule , paymentAccountRoutes } from './modules/payment-account/payment-account.module';
import { ErrorNotificationInterceptor } from './modules/error-handler/error-notification.interceptor';
import { ErrorNotificationModule } from './modules/error-handler/error-notification.module';


let routes = [
	{ path : '*' , redirectTo : '' } ,
	{ path : '' , component : HomeComponent , pathMatch : 'full' } ,
	{ path : 'sign_in' , component : SignInComponent } ,
	{ path : 'sign_up' , component : SignUpComponent } ,
	{
		path : 'payment_accounts' , component : PaymentAccountComponent , children : paymentAccountRoutes
	}
];

@NgModule ( {
	declarations :  [
		AppComponent ,
		NavMenuComponent ,
		HomeComponent ,
		CounterComponent ,
		FetchDataComponent ,
		SignInComponent ,
		SignUpComponent
		// TODO Extract sign in and sign up as separate module
	] , imports :   [
		ErrorNotificationModule,
		BrowserModule.withServerTransition ( { appId : 'ng-cli-universal' } ) ,
		HttpClientModule ,
		FormsModule ,
		PaymentAccountModule ,
		RouterModule.forRoot ( routes )
	] , providers : [
		{
			provide : HTTP_INTERCEPTORS , useClass : ErrorNotificationInterceptor , multi : true
		}
	] , bootstrap : [ AppComponent ]
} )
export class AppModule {}
