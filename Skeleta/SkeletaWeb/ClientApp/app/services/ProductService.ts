import { IProduct } from "../components/products/product";
import { Injectable } from "@angular/core";
//import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class ProductService {
	private readonly _productsUrl: string = "/api/products";

	//constructor(private httpClient: HttpClientModule) { }

	//getProducts(): Observable<IProduct[]> {
	//	return this.httpClient.get<IProduct[]>(this._productsUrl);
	//}
}; 