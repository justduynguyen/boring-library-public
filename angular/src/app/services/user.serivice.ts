import { Injectable } from '@angular/core';
import { Login } from '../models/login.model';
import { Register } from '../models/register.model';
import { User } from '../models/user.model';
import { DataService } from './data.service';

@Injectable({
	providedIn: 'root',
})
export class UserService {
	constructor(public dataService: DataService) {}
	currentUserCredit: number = 0;

	login(data: Login) {
		return this.dataService.post<Login, User>('api/app/custom-user/login', data, true, 'Login Successfully');
	}

	register(data: Register) {
		return this.dataService.post<Register, null>('api/app/custom-user/register', data, true, 'Register Successfully');
	}

	getCredit() {
		if (this.isLogged) {
			this.dataService.fetch<number>('api/app/custom-user/credit').subscribe(credit => {
				this.currentUserCredit = credit;
			});
		}
	}

	get isLogged() {
		return localStorage.getItem('user')?.length ? true : false;
	}
}
