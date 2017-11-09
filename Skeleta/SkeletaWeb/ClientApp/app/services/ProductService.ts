import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { FormGroup, FormControl } from "@angular/forms";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

@Injectable()
export class ProductService {
	private readonly _baseUrl: string = "/api/products";

	constructor(private http: HttpClient) { }

	getProducts(): Observable<Product[]> {
		return this.http.get<Product[]>(this._baseUrl)
			.do(data => console.log('All: ' + JSON.stringify(data)))
			.catch(this.handleError);
	}

	getPagedProducts() {
		//todo
	}

	getProduct(id: number): Observable<Product> {
		const url = `${this._baseUrl}/${id}`;
		return this.http.get<Product>(url)
			.catch(this.handleError);
	}

	updateProduct(product: Product): Observable<Product> {
		const url = `${this._baseUrl}/${product.id}`;
		return this.http.put(url, product)
			.catch(this.handleError);
	}	

	addProduct(product: Product): Observable < Product > {
		return this.http.post(this._baseUrl, product)
			.catch(this.handleError);
	}

	deleteProduct(id: number): Observable<Product> {
		const url = `${this._baseUrl}/${id}`;
		return this.http.delete<Product>(url)
			.catch(this.handleError);
	}

	private handleError(err: HttpErrorResponse) {
		console.log(err.message);
		return Observable.throw(err.message);
	}
}

export class Product {
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