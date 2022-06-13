import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import {
  PaymentAccountListComponent
} from './modules/payment-account/components/payment-account-list/payment-account-list.component';
import {
  PaymentAccountItemComponent
} from './modules/payment-account/components/payment-account-list/payment-account-item/payment-account-item.component';
import {
  PaymentAccountExpandedItemComponent
} from './modules/payment-account/components/payment-account-expanded-item/payment-account-expanded-item.component';
import { PaymentAccountComponent } from './modules/payment-account/payment-account.component';
import { PaymentAccountModule , paymentAccountRoutes } from './modules/payment-account/payment-account.module';



let routes = [
  { path : '*' , redirectTo : '' } ,
  { path : '' , component : HomeComponent , pathMatch : 'full' } ,
  { path : 'sign_in' , component : SignInComponent } ,
  { path : 'sign_up' , component : SignUpComponent } ,
  {
    path : 'payment_accounts' , component : PaymentAccountComponent , children : paymentAccountRoutes
  }
];
@NgModule( {
  declarations : [
    AppComponent ,
    NavMenuComponent ,
    HomeComponent ,
    CounterComponent ,
    FetchDataComponent ,
    SignInComponent ,
    SignUpComponent
    // TODO Extract sign in and sign up as separate module
  ] ,
  imports : [
    BrowserModule.withServerTransition( { appId : 'ng-cli-universal' } ) ,
    HttpClientModule ,
    FormsModule ,
    PaymentAccountModule ,
    RouterModule.forRoot( routes )
  ] ,
  providers : [] ,
  bootstrap : [ AppComponent ]
} )
export class AppModule {}
