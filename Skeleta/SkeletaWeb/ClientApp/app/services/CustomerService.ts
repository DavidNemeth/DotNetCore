import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

@Injectable()
export class CustomerService {
	private readonly _baseUrl: string = "/api/customers";

	constructor(private http: HttpClient) { }

	getCustomers(): Observable<ICustomer[]> {
		return this.http.get<ICustomer[]>(this._baseUrl)
			.do(data => console.log('All: ' + JSON.stringify(data)))
			.catch(this.handleError);
	}

	getPagedCustomers() {
		//todo
	}

	getCustomer(id: number): Observable<ICustomer> {
		const url = `${this._baseUrl}/${id}`;
		return this.http.get<ICustomer>(url)
			.catch(this.handleError);
	}

	updateCustomer(product: ICustomer): Observable<ICustomer> {
		const url = `${this._baseUrl}/${product.id}`;
		return this.http.put(url, product)
			.catch(this.handleError);
	}

	addCustomer(product: ICustomer) {
		return this.http.post<ICustomer>(this._baseUrl, product)
			.catch(this.handleError);
	}

	deleteCustomer(id: number): Observable<ICustomer> {
		const url = `${this._baseUrl}/${id}`;
		return this.http.delete<ICustomer>(url)
			.catch(this.handleError);
	}

	private handleError(err: HttpErrorResponse) {
		console.log(err.message);
		return Observable.throw(err.message);
	}
}

export interface ICustomer {
	id: number;
	firstName: string;
	lastName: string;
	email: string;
}
export class Customer implements ICustomer {
	id: number;
	firstName: string;
	lastName: string;
	email: string;

	get FirstName(): string {
		return this.firstName;
	}

	set LastName(value: string) {
		this.lastName = value;
	}

	get Email(): string {
		return this.email;
	}
}