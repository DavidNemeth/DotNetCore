import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from "@angular/forms";
import { ICustomer, CustomerService, Customer } from "../../services/CustomerService";

@Component({
	selector: 'app-customer',
	templateUrl: './customer.component.html',
	styleUrls: ['./customer.component.css']
})
/** customer component*/
export class CustomerComponent implements OnInit {
	customer: Customer = new Customer();
	customers: ICustomer[];	
	firstname: string;
	customerForm: FormGroup;
	errorMessage: string;
	/** customer ctor */
	constructor(private _customerService: CustomerService, private fb: FormBuilder) { }

	ngOnInit(): void {
		this._customerService.getCustomers()
			.subscribe(customers => {
				this.customers = customers;
			},
			error => this.errorMessage = <any>error);

		this.customerForm = this.fb.group({
			firstName: '',
			lastName: '',
			email: ''			
		});
	}	

	get Customer(): Customer {
		return this.customer;
	}

	set Customer(value: Customer) {
		this.customer.firstName = value.firstName;
		this.firstname = value.firstName;
	}


	UpdateCustomer() {
		this._customerService.updateCustomer(this.customer).subscribe();
	}
	AddCustomer() {		
		this._customerService.addCustomer(this.customer).subscribe();
	}
	DeleteCustomer() {
		this._customerService.deleteCustomer(this.customer.id).subscribe();
	}
}