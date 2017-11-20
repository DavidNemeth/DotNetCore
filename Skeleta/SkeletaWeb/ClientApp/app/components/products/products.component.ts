import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ProductService, Product } from "../../services/ProductService";
import { MatTableDataSource, MatPaginator, PageEvent, MatSort } from "@angular/material";
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
	showImage = true;
	pageTitle: string;
	loadInfo: string;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	product: Product;
	products = new MatTableDataSource(new Array<Product>());
	displayedColumns = ['Name', 'Code', 'Price', 'Rating'];	
	// MatPaginator Inputs	
	pageSize = 10;
	length;
	pageSizeOptions = [5, 10, 25, 100];
	pageEvent: PageEvent;
	isLoaded = false;
	constructor(private service: ProductService) { }

	ngAfterViewInit() {		
		this.loadProducts();	
		this.products.paginator = this.paginator;
		this.products.sort = this.sort;
		this.showImage = true;
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
		this.showImage = false;
	}
}