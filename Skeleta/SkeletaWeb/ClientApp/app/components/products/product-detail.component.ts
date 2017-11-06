import { Component, OnInit } from '@angular/core';
import { IProduct } from "../../services/ProductService";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
	selector: 'app-product-detail',
	templateUrl: './product-detail.component.html',
	styleUrls: ['./product-detail.component.css']
})
/** product-detail component*/
export class ProductDetailComponent implements OnInit {	
	pageTitle: string = 'Product Detail';
	product: IProduct;
	constructor(private _route: ActivatedRoute, private _router: Router) { }

	ngOnInit(): void {
		let id = +this._route.snapshot.paramMap.get('id');
		this.pageTitle += `: ${id}`;
		this.product = {
			"id": id,
			"name": "GNU Dany Monster",
			"code": "GNU-2132",
			"description": "Its shiny and expensive, must be really good please buy it.",
			"price": 423.99,
			"rating": 4.6,
			"imageUrl": "https://openclipart.org/download/20579/maidis-snowboard-2.svg"
		}
	}

	onBack(): void {
		this._router.navigate(['/products']);
	}
	
}