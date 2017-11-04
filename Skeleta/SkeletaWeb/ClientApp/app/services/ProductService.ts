import { IProduct } from "../components/products/product";
import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

@Injectable()
export class ProductService {
	private readonly _productsUrl: string = "/api/products";

	constructor(private http: HttpClient) { }

	getProducts(): Observable<IProduct[]> {
		return this.http.get<IProduct[]>(this._productsUrl)
			.do(data => console.log('All: ' + JSON.stringify(data)))
			.catch(this.handleError);
	}

	private handleError(err: HttpErrorResponse) {
		console.log(err.message);
		return Observable.throw(err.message);
	}
}