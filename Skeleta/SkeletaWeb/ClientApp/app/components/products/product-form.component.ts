import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { Product, ProductService } from "../../services/ProductService";
import { Observable } from 'rxjs/Observable';

@Component({
	selector: 'app-product-form',
	templateUrl: './product-form.component.html'
})
/** product-form component*/
export class ProductFormComponent implements OnInit {
	@ViewChild('readOnlyTemplate') readOnlyTemplate: TemplateRef<any>;
	@ViewChild('editTemplate') editTemplate: TemplateRef<any>;
	product: Product;
	selProduct: Product;
	products: Array<Product>;
	isNewRecord: boolean;
	statusMessage: string;

	constructor(private service: ProductService) {
		this.products = new Array<Product>();
	}

	ngOnInit(): void {
		this.loadProducts();
	}

	private loadProducts() {
		this.service.getProducts()
			.subscribe(products => {
				this.products = products;
			});
	}

	addProduct() {
		this.selProduct = new Product(0, '', '', '', 0, 0, '');
		this.products.push(this.selProduct);
		this.isNewRecord = true;
		//return this.editTemplate;
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
				this.products.splice(this.products.indexOf(product),1);
		});

	}
}