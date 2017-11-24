import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { FormGroup, FormControl } from "@angular/forms";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/timeout';

@Injectable()
export class ProductService {
	private readonly _baseUrl: string = "/api/products";

	constructor(private http: HttpClient) { }

	getProducts(): Observable<IProduct[]> {
		return this.http.get<IProduct[]>(this._baseUrl)
			.do(data => console.log('All: ' + JSON.stringify(data)))
			.catch(this.handleError);

	}

	getPagedProducts() {
		//todo
	}

	getProduct(id: number): Observable<IProduct> {
		const url = `${this._baseUrl}/${id}`;
		return this.http.get<IProduct>(url)
			.catch(this.handleError);
	}

	updateProduct(product: IProduct): Observable<IProduct> {
		const url = `${this._baseUrl}/${product.id}`;
		return this.http.put(url, product)
			.catch(this.handleError);
	}

	addProduct(product: IProduct): Observable<IProduct> {
		return this.http.post(this._baseUrl, product)
			.catch(this.handleError);
	}

	deleteProduct(id: number): Observable<IProduct> {
		const url = `${this._baseUrl}/${id}`;
		return this.http.delete<IProduct>(url)
			.catch(this.handleError);
	}

	private handleError(err: HttpErrorResponse) {
		console.log(err.message);
		return Observable.throw(err.message);
	}
}

export interface IProduct {
	id: number;
	name: string;
	code: string;
	description: string;
	price: number;
	rating: number;
	imageUrl: string;
}

export class Product implements IProduct{
	constructor(
		public id: number,
		public name: string,
		public code: string,
		public description: string,
		public price: number,
		public rating: number,
		public imageUrl: string
	) { }
}