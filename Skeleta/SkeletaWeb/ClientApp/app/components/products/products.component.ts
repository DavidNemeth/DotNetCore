import { Component, OnInit, ViewChild, TemplateRef, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { Product, ProductService, IProduct } from "../../services/ProductService";
import { Observable } from 'rxjs/Observable';
import { DatatableComponent } from "@swimlane/ngx-datatable/release";
import { timer } from "../../services/commonServices";
import { MatDialog } from "@angular/material";
import { ConfirmComponent } from '../shared/confirm.component';
import { ProductEditComponent } from './product-edit.component';

@Component({
	selector: 'app-product-form',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.css'],
	encapsulation: ViewEncapsulation.None
})
/** product-form component*/
export class ProductsComponent implements OnInit {	
	isNewRecord: boolean;
	canRestore: boolean;
	loadInfo: string;
	pageTitle: string = "Products";
	product: IProduct;
	tempProduct: IProduct;
	@ViewChild(DatatableComponent) productsTable: DatatableComponent;
	expanded: any = {};
	products: IProduct[];
	filteredByName: IProduct[];
	selected = [];
	isLoaded = false;

	constructor(private service: ProductService, public dialog: MatDialog) {
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
				this.products = products.sort(p=>p.createdDate);
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
	

	deleteProduct(product: Product) {
		this.service.deleteProduct(product.id)
			.subscribe(product => {
				this.products = this.products.filter(item => item.id !== product.id);
				for (var i = 0; i < this.products.length; i++) {
					var current = this.products[i];
					if (current.id === product.id) {
						this.products.splice(i, 1);
						break;
					}
				}
			},
			error => {
				this.pageTitle = error;
			}
			);
	
	}

	updateProduct(product: IProduct) {
		this.service.updateProduct(product)
			.subscribe(product => {		
				this.pageTitle = `Product Successfully Edited!`;
			},
			error => {
				this.pageTitle = error;
			}
			);
		
	}

	addProduct() {
		this.service.addProduct(this.product)
			.subscribe(product => {
				this.pageTitle = `Product Successfully Added!`;
				this.products.push(this.product);
				this.loadProducts();				
			},
			error => {
				this.pageTitle = error;
			
			}
			);
	}

	/* Dialogs */

	deleteDialog(product): void {
		let dialogRef = this.dialog.open(ConfirmComponent, {
			width: '250px',
			data: { message: `Delete ${product.name}?` }
		});

		dialogRef.afterClosed().subscribe(result => {
			console.log('deleteDialog was closed');
			if (result)
				this.deleteProduct(product);
		});
	}

	editDialog(product): void {		
		let dialogRef = this.dialog.open(ProductEditComponent, {
			width: '800px',
			data: { product: product }
		});

		dialogRef.afterClosed().subscribe(result => {
			console.log('editDialog was closed');
			if (result)
				this.updateProduct(product);
			else
				this.loadProducts();
		});
	}

	addDialog(): void {
		this.product = new Product(0, '', '', '', 0, 0, '', new Date())
		let dialogRef = this.dialog.open(ProductEditComponent, {
			width: '800px',
			data: { product: this.product }
		});

		dialogRef.afterClosed().subscribe(result => {
			console.log('editDialog was closed');
			if (result)
				this.addProduct();			
		});
	}
}