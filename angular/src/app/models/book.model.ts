export interface BookPaging {
	totalCount: number;
	items: Book[];
}

export interface Book {
	name: string;
	credit: number;
	quantity: number;
	id: string;
}

export interface BookBorrowedPaging {
	totalCount: number;
	items: BookBorrowed[];
}

export interface BookBorrowed {
	id: string;
	userId: string;
	bookId: string;
	bookName: string;
	bookCredit: number;
	dueDate: string;
}

export interface CreateBook {
	name: string;
	credit: number;
	quantity: number;
}
