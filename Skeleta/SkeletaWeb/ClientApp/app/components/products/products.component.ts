import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { IProduct } from "./product";

@Component({
	selector: 'products',
	templateUrl: './products.component.html',
	styleUrls: ['./products.component.css'],
	animations: [fadeInOut]
})
export class ProductsComponent implements OnInit {

	constructor() {
		this.filteredProducts = this.products;
	}		

	pageTitle: string = 'Product List';	
	showImage: boolean = false;
	imageWidth: number = 50;
	imageMargin: number = 2;

	_listFilter: string;
	get listFilter(): string {
		return this._listFilter
	}
	set listFilter(value: string) {
		this._listFilter = value;
		this.filteredProducts = this.listFilter ? this.performFilter(this.listFilter) : this.products;
	}

	filteredProducts: IProduct[];
	products: IProduct[] = [
		{
			Id: 5,
			Name: "GNU Dany Monster",
			Code: "GNU-9231",
			Description: "We payed Dany a lot to sponsor this, so please buy it.",
			Price: 423.99,
			Rating: 4.9,
			imageUrl: "https://openclipart.org/download/20579/maidis-snowboard-2.svg"
		},			
		{
			Id: 7,
			Name: "Nitro Women Secret",
			Code: "NTR-1123",
			Description: "Rocker freestyle, anti catch edge",
			Price: 573.99,
			Rating: 3.7,
			imageUrl: "https://openclipart.org/download/20580/maidis-snowboard-3.svg"
		},			
		{
			Id: 11,
			Name: "Rossignol Retox Amptek",
			Code: "ROS-0023",
			Description: "Hyibrid camber with Amptek Tech.",
			Price: 399.99,
			Rating: 4.4,
			imageUrl: "https://openclipart.org/download/20581/maidis-snowboard-4.svg"
		},
		{
			Id: 2,
			Name: "Burton Custom Flying V",
			Code: "BTN-0143",
			Description: "Hybrid High-end with channel binding slot.",
			Price: 623.99,
			Rating: 2.4,
			imageUrl: "https://openclipart.org/download/20578/maidis-snowboard-1.svg"
		}
	];

	performFilter(filterBy: string): IProduct[] {
		filterBy = filterBy.toLocaleLowerCase();
		return this.products.filter((product: IProduct) =>
			product.Name.toLocaleLowerCase().indexOf(filterBy) !== -1);		
	}

	ToggleImage(): void{
		this.showImage = !this.showImage;	
	}

	ngOnInit(): void {
		console.log('OnInit()');
	}
}