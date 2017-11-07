﻿import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

@Injectable()
export class ProductService {
	private readonly _baseUrl: string = "/api/products";

	constructor(private http: HttpClient) { }

	getProducts(): Observable<IProduct[]> {
		return this.http.get<IProduct[]>(this._baseUrl)
			.do(data => console.log('All: ' + JSON.stringify(data)))
			.catch(this.handleError);
	}

	getProduct(id: number): Observable<IProduct> {		
		const url = `${this._baseUrl}/${id}`;
		return this.http.get<IProduct>(url)
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