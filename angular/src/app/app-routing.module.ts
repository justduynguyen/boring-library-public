import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { BookComponent } from './components/books/book.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { LoggedGuard } from './guards/logged.guard';

const routes: Routes = [
	{ path: '', redirectTo: 'books', pathMatch: 'full' },
	{ path: 'login', component: LoginComponent, canActivate: [LoggedGuard] },
	{ path: 'register', component: RegisterComponent, canActivate: [LoggedGuard] },
	{ path: 'books', component: BookComponent, canActivate: [AuthGuard] },
	{ path: '**', redirectTo: 'books' },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
