import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './input/input.component';
import { ControlValidationComponent } from './control-validation/control-validation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SelectComponent } from './select/select.component';


@NgModule ( {
	declarations : [ InputComponent , ControlValidationComponent , SelectComponent ] ,
	exports :      [ InputComponent , SelectComponent ] ,
	imports :      [ CommonModule , ReactiveFormsModule ]
} )
export class CommonControlsModule {}
