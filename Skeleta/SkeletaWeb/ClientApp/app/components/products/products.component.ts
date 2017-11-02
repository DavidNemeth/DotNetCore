import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { IProduct } from "./product";
import { ProductService } from "../../services/ProductService";

@Component({
	selector: 'products',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.css'],
	animations: [fadeInOut]
})
export class ProductsComponent implements OnInit {

	constructor(private _productService: ProductService) {		
	}

	ngOnInit(): void {
		this.products = this._productService.getProducts();
		this.filteredProducts = this.products;
	}

	onRatingClicked(message: string): void {
		this.pageTitle = 'Product List:' + message;
	}

	pageTitle: string = 'Product List';
	showImage: boolean = false;
	imageWidth: number = 50;
	imageMargin: number = 2;

	_listFilter: string;
	get listFilter(): string {
		return this._listFilter
	}
	set listFilter(value: string) {
		this._listFilter = value;
		this.filteredProducts = this.listFilter ? this.performFilter(this.listFilter) : this.products;
	}

	filteredProducts: IProduct[];
	products: IProduct[] = [];

	performFilter(filterBy: string): IProduct[] {
		filterBy = filterBy.toLocaleLowerCase();
		return this.products.filter((product: IProduct) =>
			product.Name.toLocaleLowerCase().indexOf(filterBy) !== -1);		
	}

	ToggleImage(): void{
		this.showImage = !this.showImage;	
	}	
}