import { Component, ViewEncapsulation } from '@angular/core';
import { CustomerService } from "../../services/CustomerService";
import { ProductService } from "../../services/ProductService";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PubSubService } from '../../services/pub-sub.service';

@Component({
	selector: 'skeleta-app',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css', '../../style.css'],
	encapsulation: ViewEncapsulation.None,
	providers: [
		ProductService,
		PubSubService,
		CustomerService
	]
})
export class AppComponent {
	pagetitle = 'Skeleta Template App';
}