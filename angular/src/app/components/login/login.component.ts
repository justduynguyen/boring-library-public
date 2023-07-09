import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login.model';
import { UserService } from 'src/app/services/user.serivice';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
	validateForm!: UntypedFormGroup;

	constructor(private fb: UntypedFormBuilder, private userService: UserService, private router: Router) {
		if (userService.isLogged) {
			this.router.navigateByUrl('/books');
		}
	}

	ngOnInit(): void {
		this.validateForm = this.fb.group({
			userName: [null, [Validators.required]],
			password: [null, [Validators.required]],
		});
	}

	submitForm(): void {
		if (this.validateForm.valid) {
			this.userService.login(this.validateForm.value as Login).subscribe(user => {
				localStorage.setItem('user', JSON.stringify(user));
				this.userService.getCredit();
				this.router.navigateByUrl('/books');
			});
		} else {
			Object.values(this.validateForm.controls).forEach(control => {
				if (control.invalid) {
					control.markAsDirty();
					control.updateValueAndValidity({ onlySelf: true });
				}
			});
		}
	}
}
