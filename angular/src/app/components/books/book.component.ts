import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzTabChangeEvent } from 'ng-zorro-antd/tabs';
import { Book, BookBorrowed, CreateBook } from 'src/app/models/book.model';
import { BookService } from 'src/app/services/book.service';
import { UserService } from 'src/app/services/user.serivice';

interface Tab {
	name: string;
	value: string;
}

@Component({
	selector: 'app-book',
	templateUrl: './book.component.html',
	providers: [BookService],
})
export class BookComponent implements OnInit {
	books: Book[] = [];
	booksTotal: number = 0;
	booksBorrowed: BookBorrowed[] = [];
	booksBorrowedTotal: number = 0;
	isVisibleAddBookForm = false;
	addBookForm!: UntypedFormGroup;

	borrowedTabs: Tab[] = [
		{
			name: 'Approaching expiration date',
			value: 'ApproachingExpDate',
		},
		{
			name: 'Overdue date',
			value: 'Overdue',
		},
		{
			name: 'Within Due Date',
			value: 'WithinDueDate',
		},
	];

	borrowedTabIndex = 0;

	constructor(private bookService: BookService, private userService: UserService, private fb: UntypedFormBuilder) {}

	ngOnInit(): void {
		this.addBookForm = this.fb.group({
			name: [null, [Validators.required]],
			credit: [null, [Validators.required, Validators.min(1)]],
			quantity: [null, [Validators.required, Validators.min(1)]],
		});
	}

	getBookList(skip: number = 0, take: number = 10) {
		this.bookService.getBookList(skip, take).subscribe(books => {
			this.books = books.items || [];
			this.booksTotal = books.totalCount;
		});
	}

	getBookBorrowed(skip: number = 0, take: number = 10, filter: string = 'close') {
		this.bookService.getBookBorrowed(skip, take, filter).subscribe(books => {
			this.booksBorrowed = books.items || [];
			this.booksBorrowedTotal = books.totalCount;
		});
	}

	borrow(book: Book) {
		this.bookService.borrow(book.id).subscribe(() => {
			book.quantity--;
			this.getBookBorrowed(0, 10, this.borrowedTabs[this.borrowedTabIndex].value);
		});
	}

	onQueryParamsChangeAvailableBooks(params: NzTableQueryParams) {
		console.log(params);
		const { pageSize, pageIndex, sort, filter } = params;
		const currentSort = sort.find(item => item.value !== null);
		this.getBookList((pageIndex - 1) * pageSize, pageSize);
	}

	onQueryParamsChangeBorrowingBooks(params: NzTableQueryParams) {
		console.log(params);
		const { pageSize, pageIndex, sort, filter } = params;
		const currentSort = sort.find(item => item.value !== null);
		this.getBookBorrowed((pageIndex - 1) * pageSize, pageSize, this.borrowedTabs[0].value);
	}

	showAddBook() {
		this.isVisibleAddBookForm = true;
	}

	submitAddBook() {
		if (this.addBookForm.valid) {
			const submitData = this.addBookForm.value as CreateBook;
			this.bookService.createBook(submitData).subscribe(newBook => {
				this.books = [newBook, ...this.books];
				this.isVisibleAddBookForm = false;
			});
		}
	}

	cancelAddBook(): void {
		this.addBookForm.reset();
		this.isVisibleAddBookForm = false;
	}

	onChangeBorrowBookTab(tab: NzTabChangeEvent) {
		this.borrowedTabIndex = tab.index!;
		this.getBookBorrowed(0, 10, this.borrowedTabs[this.borrowedTabIndex].value);
	}
}
