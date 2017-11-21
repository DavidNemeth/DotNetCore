import { Component, OnInit, ViewChild, TemplateRef, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { Product, ProductService } from "../../services/ProductService";
import { Observable } from 'rxjs/Observable';
import { DatatableComponent } from "@swimlane/ngx-datatable/release";
import { timer } from "../../services/commonServices";
import { MatDialog } from "@angular/material";
import { ProductModalComponent } from "./product-modal.component";

@Component({
	selector: 'app-product-form',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.css'],
	encapsulation: ViewEncapsulation.None
})
/** product-form component*/
export class ProductsComponent implements OnInit {
	canRestore: boolean;
	loadInfo: string;
	pageTitle: string = "Products";
	@ViewChild('readOnlyTemplate') readOnlyTemplate: TemplateRef<any>;
	@ViewChild('editTemplate') editTemplate: TemplateRef<any>;
	product: Product;
	@ViewChild(DatatableComponent) productsTable: DatatableComponent;
	expanded: any = {};
	products: Product[];
	filteredByName: Product[];
	selected = [];
	isLoaded = false;

	constructor(private service: ProductService, public editModal: MatDialog) {
		this.products = new Array<Product>();
	}

	ngOnInit(): void {
		this.loadProducts();
	}

	private loadProducts() {
		this.isLoaded = false;
		this.pageTitle = "Loading..";
		let loadTime = timer();
		this.service.getProducts()
			.subscribe(products => {
				this.filteredByName = [...products];
				this.products = products;
				this.isLoaded = true;
				this.pageTitle = "Products";
				this.loadInfo = `${this.products.length} Records loaded in ${loadTime.seconds}`;
			},
			error => {
				this.loadInfo = "Offline mode active - Your work will be saved!";
				this.pageTitle = "Disconnected - Attempting to Reconnect..";
				this.Reconnect();
			});
	}

	private Reconnect() {
		this.service.getProducts()
			.subscribe(products => {
				setTimeout(() => {
					this.filteredByName = [...products];
					this.products = products;
					this.isLoaded = true;
					this.loadInfo = `Successfully Reconnected! - Changes synced`;
					this.pageTitle = "Products";
				}, 5000)
			},
			error => {
				this.Reconnect();
			});
	}

	updateFilter(event) {
		const val = event.target.value.toLowerCase();
		const filteredByName = this.filteredByName.filter(function (d) {
			return d.name.toLowerCase().indexOf(val) !== -1 || !val;
		});
		this.products = filteredByName;
		this.productsTable.offset = 0;
	}

	toggleExpandRow(row) {
		this.productsTable.rowDetail.toggleExpandRow(row);
		let product: Product = row;
	}

	onSelect({ selected }) {
		console.log('Select Event', selected, this.selected);
		this.selected.splice(0, this.selected.length);
		this.selected.push(...selected);
	}

	//*Client side Ops*//
	deleteProduct(product: Product) {
		this.products = this.products.filter(item => item.id !== product.id);
	}

	updateProduct(product: Product) {

	}

	addProduct() {

	}

	editProduct(product): void {
		this.product = product;
		let dialogRef = this.editModal.open(ProductModalComponent, {
			width: '250px',
			data: { product: product }
		});

		dialogRef.afterClosed().subscribe(result => {
			console.log('The dialog was closed');
			product = this.product;
		});
	}
}