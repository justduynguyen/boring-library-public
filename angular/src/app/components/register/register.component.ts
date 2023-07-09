import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Register } from 'src/app/models/register.model';
import { UserService } from 'src/app/services/user.serivice';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {
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
			this.userService.register(this.validateForm.value as Register).subscribe(() => {
				this.router.navigateByUrl('/login');
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
