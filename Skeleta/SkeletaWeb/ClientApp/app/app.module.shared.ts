import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProductsComponent } from './components/products/products.component';
import { ProductDetailComponent } from './components/products/product-detail.component';
import { ConvertToSpacePipe } from "./pipes/convert-to-space.pipe";
import { GroupByPipe } from './pipes/group-by.pipe';
import { StarComponent } from './components/shared/star.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CustomerComponent } from "./components/customer/customer.component";
import { ProductFormComponent } from "./components/products/product-form.component";

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		ProductsComponent,
		CustomerComponent,
		ProductFormComponent,
		ProductDetailComponent,
		GroupByPipe,
		ConvertToSpacePipe,
		StarComponent		
	],
	imports: [
		ModalModule.forRoot(),
		ReactiveFormsModule,
		CommonModule,
		HttpClientModule,
		FormsModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: HomeComponent },			
			{ path: 'products', component: ProductsComponent },
			{ path: 'products/:id', component: ProductDetailComponent },
			{ path: 'customer', component: CustomerComponent },
			{ path: 'productForm', component: ProductFormComponent },
			{ path: '**', redirectTo: 'home' }
			
		])
	]
})
export class AppModuleShared {
}