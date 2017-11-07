﻿import { Component, OnInit } from '@angular/core';
import { IProduct, ProductService } from "../../services/ProductService";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
	selector: 'app-product-detail',
	templateUrl: './product-detail.component.html',
	styleUrls: ['./product-detail.component.css']
})
/** product-detail component*/
export class ProductDetailComponent implements OnInit {
	errorMessage: any;
	pageTitle: string = 'Product Detail';
	product: IProduct;
	constructor(private _productService: ProductService, private _route: ActivatedRoute, private _router: Router) { }

	ngOnInit(): void {
		let id = +this._route.snapshot.paramMap.get('id');
		this._productService.getProduct(id)
			.subscribe(product => {
				this.product = product;
			},
			error => this.errorMessage = <any>error);
	}

	onBack(): void {
		this._router.navigate(['/products']);
	}

}