import { Component , Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component( {
  selector : 'app-home' ,
  templateUrl : './home.component.html'
} )
export class HomeComponent {
  public get IsAuthenticated() : boolean {
    return localStorage.getItem( 'token' ) !== null;
  }

  public get UserName() : string {
    return localStorage.getItem( 'userName' ) ?? '';
  }

  constructor( private router : Router , private authenticationService : AuthenticationService ) {}

  public signIn() {
    this.router.navigate( [ '/sign_in' ] );
  }

  public signUp() {
    this.router.navigate( [ '/sign_up' ] );
  }

  public signOut() {
    localStorage.removeItem( 'token' );
    localStorage.removeItem( 'userName' );
  }
}
