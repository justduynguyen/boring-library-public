import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard implements CanActivate {
	constructor(private router: Router) {}

	canActivate(): boolean {
		const user = localStorage.getItem('user');

		if (user) {
			// User is logged in
			return true;
		} else {
			// User is not logged in, redirect to login page
			this.router.navigate(['/login']);
			return false;
		}
	}
}
