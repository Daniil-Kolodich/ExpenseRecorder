import { Component } from '@angular/core';

@Component ( {
	selector : 'app-nav-menu' , templateUrl : './nav-menu.component.html' , styleUrls : [ './nav-menu.component.scss' ]
} )
export class NavMenuComponent {
	isExpanded = false;

	public isAuthenticated () : boolean {
		return localStorage.getItem ( 'token' ) !== null;
	}

	collapse () {
		this.isExpanded = false;
	}

	toggle () {
		this.isExpanded = !this.isExpanded;
	}
}
