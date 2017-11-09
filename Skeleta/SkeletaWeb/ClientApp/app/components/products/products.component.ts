import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ProductService, Product } from "../../services/ProductService";

@Component({
	selector: 'app-products',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.css'],
	animations: [fadeInOut]
})
/** products component*/
export class ProductsComponent implements OnInit {

	constructor(private _productService: ProductService) {

	}

	pageTitle: string = 'Product List';
	showImage: boolean = false;
	imageWidth: number = 50;
	imageMargin: number = 2;
	errorMessage: string;
	_listFilter: string;

	ngOnInit(): void {
		this._productService.getProducts()
			.subscribe(products => {
				this.products = products;
				this.filteredProducts = this.products;
			},
			error => this.errorMessage = <any>error);
	}

	onRatingClicked(message: string): void {
		this.pageTitle = 'Product List:' + message;
	}

	get listFilter(): string {
		return this._listFilter
	}

	set listFilter(value: string) {
		this._listFilter = value;
		this.filteredProducts = this.listFilter ? this.performFilter(this.listFilter) : this.products;
	}

	filteredProducts: Product[];
	products: Product[] = [];

	performFilter(filterBy: string): Product[] {
		filterBy = filterBy.toLocaleLowerCase();
		return this.products.filter((product: Product) =>
			product.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
	}

	ToggleImage(): void {
		this.showImage = !this.showImage;
	}
}