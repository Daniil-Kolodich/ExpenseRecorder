import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { FormsModule , ReactiveFormsModule } from '@angular/forms';
import { AccountComponent } from './components/account/account.component';
import { RouterModule } from '@angular/router';
import { AccountConfigComponent } from './account-config.component';
import { CommonControlsModule } from '../common-controls/common-controls.module';

export const accountRoutes = [
	{ path : '*' , redirectTo : '' } ,
	{ path : '' , component : AccountComponent , pathMatch : 'full' } ,
	{ path : 'sign_in' , component : SignInComponent } ,
	{ path : 'sign_up' , component : SignUpComponent }
];

@NgModule ( {
	declarations : [
		SignInComponent , SignUpComponent, AccountComponent, AccountConfigComponent
	] , imports : [
		CommonModule , FormsModule , RouterModule , CommonControlsModule , ReactiveFormsModule
	]
} )
export class AccountConfigModule {}
