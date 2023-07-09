import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './services/user.serivice';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
})
export class AppComponent {
	title = 'angular';
	constructor(public userService: UserService, private router: Router) {}
	ngOnInit() {
		this.userService.getCredit();
	}
	logout() {
		localStorage.clear();
		this.router.navigateByUrl('/login');
	}
}
