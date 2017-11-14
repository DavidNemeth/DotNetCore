import { Component, OnInit, ViewChild } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ProductService, Product } from "../../services/ProductService";
import { MatTableDataSource, MatPaginator, PageEvent } from "@angular/material";

@Component({
	selector: 'app-products',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.css'],
	animations: [fadeInOut]
})
/** products component*/
export class ProductsComponent {
	@ViewChild(MatPaginator) paginator: MatPaginator;
	product: Product;
	products: Array<Product>;
	displayedColumns = ['Name', 'Code', 'Price', 'Rating'];
	dataSource = new MatTableDataSource(this.products);
	// MatPaginator Inputs	
	pageSize = 10;
	pageSizeOptions = [5, 10, 25, 100];
	// MatPaginator Output
	pageEvent: PageEvent;
	isLoaded = false;

	constructor(private service: ProductService) {
		this.products = new Array<Product>();
	}

	//ngOnInit(): void {
	//	this.loadProducts();
	//}
	ngAfterViewInit() {
		//this.loadProducts();		
	}

	applyFilter(filterValue: string) {
		filterValue = filterValue.trim(); // Remove whitespace
		filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
		this.dataSource.filter = filterValue;
	}

	setPageSizeOptions(setPageSizeOptionsInput: string) {
		this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
	}

	private loadProducts() {
		this.service.getProducts()
			.subscribe(products => {
				this.products = products;
				this.dataSource = new MatTableDataSource(products);
				this.dataSource.paginator = this.paginator;	
				this.isLoaded = true;
				setTimeout(() => {
					this.isLoaded = false;
				},40000)
			});
	}
}