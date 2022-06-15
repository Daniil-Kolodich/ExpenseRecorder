import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorNotificationComponent } from './error-notification/error-notification.component';



@NgModule( {
  declarations : [
    ErrorNotificationComponent
  ] , exports :  [
    ErrorNotificationComponent
  ] , imports :  [
    CommonModule
  ]
})
export class ErrorNotificationModule { }
