import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProductsComponent } from './components/products/products.component';
import { ConvertToSpacePipe } from "./pipes/convert-to-space.pipe";
import { GroupByPipe } from './pipes/group-by.pipe';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		ProductsComponent,
		GroupByPipe,
		ConvertToSpacePipe
	],
	imports: [
		CommonModule,
		HttpModule,
		FormsModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: HomeComponent },			
			{ path: 'products', component: ProductsComponent },
			{ path: '**', redirectTo: 'home' }
			
		])
	]
})
export class AppModuleShared {
}