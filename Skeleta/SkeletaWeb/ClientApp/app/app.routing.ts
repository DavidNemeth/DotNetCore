import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./components/home/home.component";
import { ProductsComponent } from "./components/products/products.component";
import { CustomerComponent } from "./components/customer/customer.component";
import { ProductEditComponent } from "./components/products/product-edit.component";

const routes: Routes = [
	{ path: '', component:HomeComponent, pathMatch: 'full' },	
	{ path: 'products', component: ProductsComponent },
	{ path: 'customer', component: CustomerComponent },
	{ path: 'product/:id', component: ProductEditComponent },
	{ path: 'home', component: HomeComponent },
	{ path: '**', redirectTo: 'home' }
];

@NgModule({
	imports: [
		CommonModule,
		BrowserModule,
		RouterModule.forRoot(routes)
	],
	exports: [
		RouterModule
	],
})
export class AppRoutingModule { }
