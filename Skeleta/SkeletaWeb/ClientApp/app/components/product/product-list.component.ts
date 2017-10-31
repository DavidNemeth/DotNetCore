import { Component } from '@angular/core';

@Component({
	selector: 'products',
	templateUrl: './product-list.component.html'
})
export class ProductListComponent {
	pageTitle: string = 'Product List';



	imageWidth: number = 50;
	imageMargin: number = 2;
	products: any[] = [
		{
			"Id": 5,
			"Name": "GNU Dany Monster",
			"Code": "GNU-9231",
			"Description": "We payed Danny a lot to sponsor this, so please buy it.",
			"Price": 423.99,
			"Rating": 4.9,
			"imageUrl": "https://openclipart.org/download/20579/maidis-snowboard-2.svg"
		},			
		{
			"Id": 7,
			"Name": "Nitro Women Secret",
			"Code": "NTR-1123",
			"Description": "Rocker freestyle, anti catch edge",
			"Price": 573.99,
			"Rating": 3.7,
			"imageUrl": "https://openclipart.org/download/20580/maidis-snowboard-3.svg"
		},			
		{
			"Id": 11,
			"Name": "Rossignol Retox Amptek",
			"Code": "ROS-0023",
			"Description": "Hyibrid camber with Amptek Tech.",
			"Price": 399.99,
			"Rating": 4.4,
			"imageUrl": "https://openclipart.org/download/20581/maidis-snowboard-4.svg"
		},
		{
			"Id": 2,
			"Name": "Burton Custom Flying V",
			"Code": "BTN-0143",
			"Description": "Hybrid High-end with channel binding slot.",
			"Price": 623.99,
			"Rating": 2.4,
			"imageUrl": "https://openclipart.org/download/20578/maidis-snowboard-1.svg"
		}
	];
}