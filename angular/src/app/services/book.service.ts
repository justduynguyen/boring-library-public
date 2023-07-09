import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { Book, BookBorrowedPaging, BookPaging, CreateBook } from '../models/book.model';
import { DataService } from './data.service';
import { UserService } from './user.serivice';

@Injectable({
	providedIn: 'root',
})
export class BookService {
	constructor(private dataService: DataService, private userService: UserService) {}

	getBookList(skip: number = 0, take: number = 10) {
		return this.dataService.fetch<BookPaging>(`api/app/book?SkipCount=${skip}&&MaxResultCount=${take}`);
	}

	getBookBorrowed(skip: number = 0, take: number = 10, filter = 'close') {
		return this.dataService.fetch<BookBorrowedPaging>(`api/app/book-borrower?Filter=${filter}&SkipCount=${skip}&&MaxResultCount=${take}`);
	}

	borrow(bookId: string) {
		var data = {
			bookId: bookId,
		};
		return this.dataService.post<{}, null>('api/app/book-borrower', data).pipe(
			tap(() => {
				this.userService.getCredit();
			}),
		);
	}

	createBook(createBookDto: CreateBook) {
		return this.dataService.post<CreateBook, Book>('api/app/book', createBookDto);
	}
}
