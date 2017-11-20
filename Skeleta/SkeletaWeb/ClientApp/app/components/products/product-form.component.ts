import { Component, OnInit, ViewChild, TemplateRef, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { Product, ProductService } from "../../services/ProductService";
import { Observable } from 'rxjs/Observable';
import { DatatableComponent } from "@swimlane/ngx-datatable/release";

@Component({
	selector: 'app-product-form',
	templateUrl: './product-testForm.html',
	styleUrls: ['./product-form.component.css'],
	encapsulation: ViewEncapsulation.None
})
/** product-form component*/
export class ProductFormComponent implements OnInit {	
	pageTitle: string = "Products";
	@ViewChild('readOnlyTemplate') readOnlyTemplate: TemplateRef<any>;
	@ViewChild('editTemplate') editTemplate: TemplateRef<any>;
	product: Product;
	selProduct: Product;
	products: Array<Product>;
	isNewRecord: boolean;
	statusMessage: string;
	loadingIndicator: boolean = true;
	reorderable: boolean = true;
	showImage = false;
	@ViewChild(DatatableComponent) productsTable: DatatableComponent;
	expanded: any = {};
	rows: Product[];
	filteredByName: Product[];
	selected = [];

	constructor(private service: ProductService) {
		this.products = new Array<Product>();
	}

	ngOnInit(): void {
		this.loadProducts();
	}

	private loadProducts() {
		this.service.getProducts()
			.subscribe(products => {
				this.filteredByName = [...products];
				this.rows = products;
				this.products = products;
				this.showImage = false;
			});
	}

	updateFilter(event) {
		const val = event.target.value.toLowerCase();

		// filter data
		const filteredByName = this.filteredByName.filter(function (d) {
			return d.name.toLowerCase().indexOf(val) !== -1 || !val;
		});
		// update the rows
		this.rows = filteredByName;
		// Whenever the filter changes, always go back to the first page
		this.productsTable.offset = 0;
	}

	addProduct() {
		this.selProduct = new Product(0, '', '', '', 0, 0, '');
		this.products.push(this.selProduct);
		this.isNewRecord = true;
		//return this.editTemplate;
	}

	handlePageChange(event: any): void {
		console.log(event);
	}

	editProduct(product: Product) {
		this.selProduct = product;
	}

	loadTemplate(product: Product) {
		if (this.selProduct && this.selProduct.id == product.id) {
			return this.editTemplate;
		} else {
			return this.readOnlyTemplate;
		}
	}

	saveProduct() {
		if (this.isNewRecord) {
			this.service.addProduct(this.selProduct).subscribe(resp => {
				this.statusMessage = 'Record added Successfully.',
					this.loadProducts();
			}, error => this.statusMessage = 'Server Offline');
			this.isNewRecord = false;
			this.selProduct = null;
		}
		else {
			//edit the record
			this.service.updateProduct(this.selProduct).subscribe(resp => {
				this.statusMessage = 'Record Updated Successfully.',
					this.loadProducts();
			});
			this.selProduct = null;
		}
	}

	cancel() {
		this.selProduct = null;
	}

	deleteProduct(product: Product) {
		this.service.deleteProduct(product.id).subscribe(resp => {
			this.statusMessage = 'Record Deleted Successfully.',
				this.loadProducts();
		}, error => {
			this.statusMessage = 'Server offline, try saving changes later.',
				this.products.splice(this.products.indexOf(product), 1);
		});

	}
	
	onExpandRow(row) {
		this.productsTable.rowDetail.collapseAllRows();
		this.productsTable.rowDetail.toggleExpandRow(row);
	}
	onCollapseRow() {
		this.productsTable.rowDetail.collapseAllRows();
	}
	toggleExpandRow(row) {
		this.showImage = !this.showImage;
		this.productsTable.rowDetail.toggleExpandRow(row);
		let product: Product = row;		
	}

	

	onSelect({ selected }) {
		console.log('Select Event', selected, this.selected);
		this.selected.splice(0, this.selected.length);
		this.selected.push(...selected);		
	}

	onActivate(event) {
		console.log('Activate Event', event);
	}
}