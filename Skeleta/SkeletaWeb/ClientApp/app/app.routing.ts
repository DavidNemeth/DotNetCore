import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./components/home/home.component";
import { ProductsComponent } from "./components/products/products.component";
import { ProductDetailComponent } from "./components/products/product-detail.component";
import { CustomerComponent } from "./components/customer/customer.component";
import { ProductFormComponent } from "./components/products/product-form.component";



const routes: Routes = [
	{ path: '', redirectTo: 'home', pathMatch: 'full' },
	{ path: 'home', component: HomeComponent },
	{ path: 'products', component: ProductsComponent },
	{ path: 'products/:id', component: ProductDetailComponent },
	{ path: 'customer', component: CustomerComponent },
	{ path: 'productForm', component: ProductFormComponent },
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
