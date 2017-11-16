import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ProductService, Product } from "../../services/ProductService";
import { MatTableDataSource, MatPaginator, PageEvent } from "@angular/material";
import { timer } from "../../services/commonServices";
import { HttpErrorResponse } from "@angular/common/http";

@Component({
	selector: 'app-products',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.css'],
	animations: [fadeInOut]
})
/** products component*/
export class ProductsComponent {
	pageTitle: string;
	loadInfo: string;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	product: Product;
	displayedColumns = ['Name', 'Code', 'Price', 'Rating'];
	products = new MatTableDataSource(new Array<Product>());
	// MatPaginator Inputs	
	pageSize = 10;
	length;
	pageSizeOptions = [5, 10, 25, 100];
	// MatPaginator Outputhttp://localhost:49623/products
	pageEvent: PageEvent;
	isLoaded = false;
	constructor(private service: ProductService) { }

	//ngOnInit(): void {
	//	this.loadProducts();
	//}
	ngAfterViewInit() {		
		this.loadProducts();
	}

	applyFilter(filterValue: string) {
		filterValue = filterValue.trim(); // Remove whitespace
		filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
		this.products.filter = filterValue;
	}

	setPageSizeOptions(setPageSizeOptionsInput: string) {
		this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
	}

	private loadProducts() {
		this.isLoaded = false;
		this.pageTitle = "Loading..";
		let loadTime = timer();
		this.service.getProducts()
			.subscribe(products => {
				this.products = new MatTableDataSource(products);
				this.products.paginator = this.paginator;
				this.isLoaded = true;
				this.loadInfo = `${this.products.data.length} Records loaded in ${loadTime.seconds}`;
				this.pageTitle = "Products";
			},
			error => {
				this.loadInfo = "Offline mode active - Your work will be saved!";
				this.pageTitle = "Disconnected - Attempting to Reconnect..";
				this.Reconnect();
			}
			);
	}

	private Reconnect() {
		this.service.getProducts()
			.subscribe(products => {
				setTimeout(() => {
					this.products = new MatTableDataSource(products);
					this.products.paginator = this.paginator;
					this.isLoaded = true;
					this.loadInfo = `Successfully Reconnected! - Changes synced`;
					this.pageTitle = "Products";
				}, 5000)
			},
			error => {
				this.Reconnect();
			});
	}

	onRatingClicked(message: string): void {
		this.pageTitle = 'Product Rating: ' + message;
	}
}