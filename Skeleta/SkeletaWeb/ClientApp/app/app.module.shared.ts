import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
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

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		ProductsComponent,
		ProductDetailComponent,
		GroupByPipe,
		ConvertToSpacePipe,
		StarComponent		
	],
	imports: [
		ModalModule.forRoot(),
		CommonModule,
		HttpClientModule,
		FormsModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: HomeComponent },			
			{ path: 'products', component: ProductsComponent },
			{ path: 'products/:id', component: ProductDetailComponent },
			{ path: '**', redirectTo: 'home' }
			
		])
	]
})
export class AppModuleShared {
}