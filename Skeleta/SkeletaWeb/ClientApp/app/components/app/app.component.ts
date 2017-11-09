import { Component } from '@angular/core';
import { CustomerService } from "../../services/CustomerService";
import { ProductService } from "../../services/ProductService";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'app',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css'],
	providers: [
		ProductService,
		CustomerService
	]
})
export class AppComponent {
	pagetitle = 'Skeleta Template App';
}