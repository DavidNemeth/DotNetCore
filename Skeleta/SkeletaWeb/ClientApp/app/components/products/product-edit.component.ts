import { Component, OnInit } from '@angular/core';
import { ProductService, Product, IProduct } from "../../services/ProductService";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
	selector: 'app-product-edit',
	templateUrl: './product-edit.component.html',
	styleUrls: ['./product-edit.component.css']
})
/** product-edit component*/
export class ProductEditComponent implements OnInit {
	products: Product[];
	/** product-edit ctor */
	pageTitle: string = 'Product Detail';
	prod: IProduct;	
	constructor(private service: ProductService, private router: Router, private route: ActivatedRoute) {		
	}

	ngOnInit(): void {
		this.loadProduct();
	}

	loadProduct(): void {
		let id = +this.route.snapshot.paramMap.get('id');
		this.service.getProduct(id)
			.subscribe(product => {
				this.prod = product;
			},
			error => {
				this.pageTitle = error;
			});
	}

	cancel(): void {
		this.router.navigate(['/products']);
	}
}