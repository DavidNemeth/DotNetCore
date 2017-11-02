import { Component } from '@angular/core';
import { ProductService } from "../../services/ProductService";

@Component({
	selector: 'app',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css'],
	providers: [ ProductService ]
})
export class AppComponent {
	pagetitle = 'Skeleta Template App';
}