import { Component, ViewEncapsulation } from '@angular/core';
import { CustomerService } from "../../services/CustomerService";
import { ProductService } from "../../services/ProductService";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'skeleta-app',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css', '../../style.css'],
	encapsulation: ViewEncapsulation.None,
	providers: [
		ProductService,
		CustomerService
	]
})
export class AppComponent {
	pagetitle = 'Skeleta Template App';
}