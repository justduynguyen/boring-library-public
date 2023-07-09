import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

@Injectable({
	providedIn: 'root',
})
export class DataService {
	private baseUrl = environment.apiUrl;
	defaultSuccessMessage = 'Successfully';
	constructor(private http: HttpClient, private message: NzMessageService, private router: Router) {}

	get headers(): HttpHeaders {
		return new HttpHeaders({
			'Content-Type': 'application/json',
			Authorization: `Bearer ${localStorage.getItem('user')?.length ? (JSON.parse(localStorage.getItem('user')!) as User).token : ''}`,
		});
	}

	public fetch<T>(endpoint: string, showMessage = false, messageOnSuccess?: string): Observable<T> {
		const url = `${this.baseUrl}/${endpoint}`;
		return this.http.get<T>(url, { headers: this.headers }).pipe(
			tap(() => {
				if (showMessage) {
					this.message.create('success', messageOnSuccess ?? this.defaultSuccessMessage);
				}
			}),
			catchError((error: HttpErrorResponse) => {
				this.errorHandling(error);
				return throwError(error);
			}),
		);
	}

	public post<TIn, TOut>(endpoint: string, data: TIn, showMessage = false, messageOnSuccess?: string): Observable<TOut> {
		const url = `${this.baseUrl}/${endpoint}`;
		return this.http.post<TOut>(url, data, { headers: this.headers }).pipe(
			tap(() => {
				if (showMessage) {
					this.message.create('success', messageOnSuccess ?? this.defaultSuccessMessage);
				}
			}),
			catchError((error: HttpErrorResponse) => {
				this.errorHandling(error);
				return throwError(error);
			}),
		);
	}

	errorHandling(error: HttpErrorResponse) {
		if (error.status === 401) {
			this.message.create('error', `Something went wrong with you token.`);
			localStorage.removeItem('user');
			this.router.navigateByUrl('/login');
		}
		if (error.status === 500 || error.status === 403) {
			const errorMess =
				error.error.error.message == 'An internal error occurred during your request!'
					? error.error.error.code.length
						? error.error.error.code
						: error.error.error.message
					: error.error.error.message;
			this.message.create('error', errorMess);
		}
	}
}
