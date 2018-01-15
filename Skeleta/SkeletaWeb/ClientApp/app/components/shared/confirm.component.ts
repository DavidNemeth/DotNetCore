import { Component, EventEmitter, Output, Inject, Input } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
	selector: 'ng-confirm',
	templateUrl: './confirm.component.html',
	styleUrls: ['./confirm.component.css']
})
/** confirm component*/
export class ConfirmComponent {
	constructor(public dialogRef: MatDialogRef<ConfirmComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any) { }
}