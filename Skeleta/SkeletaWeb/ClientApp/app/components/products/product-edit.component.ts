import { Component, EventEmitter, Output, Inject, Input } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
	selector: 'app-product-edit',
	templateUrl: './product-edit.component.html',
	styleUrls: ['./product-edit.component.css']
})
/** product-edit component*/
export class ProductEditComponent {
	constructor(public dialogRef: MatDialogRef<ProductEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any) { }
}