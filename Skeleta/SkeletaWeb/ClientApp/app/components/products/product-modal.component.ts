import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { ProductsComponent } from "./products.component";
import { Product } from "../../services/ProductService";

@Component({
	selector: 'app-product-modal',
	templateUrl: './product-modal.component.html',
	styleUrls: ['./product-modal.component.css']
})
/** productModal component*/
export class ProductModalComponent {
	/** productModal ctor */
	product: Product;
	constructor(
		public dialogRef: MatDialogRef<ProductsComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any) {
		this.product = this.data.product;
	}

	onNoClick(): void {
		this.dialogRef.close();
	}
}