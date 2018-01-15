import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProductsComponent } from './components/products/products.component';
import { ConvertToSpacePipe } from "./pipes/convert-to-space.pipe";
import { GroupByPipe } from './pipes/group-by.pipe';
import { StarComponent } from './components/shared/star.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CustomerComponent } from "./components/customer/customer.component";
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from "./app.routing";
import { MaterialModule } from "./material.module";
import { FilterPipeModule } from 'ngx-filter-pipe';
import { MatDialogModule } from "@angular/material";
import { ProductEditComponent } from "./components/products/product-edit.component";
import { ConfirmComponent } from './components/shared/confirm.component';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		ProductsComponent,
		ProductEditComponent,
		CustomerComponent,
		GroupByPipe,
		ConvertToSpacePipe,
		StarComponent,
		ConfirmComponent
	],
	entryComponents: [	
		ConfirmComponent
	],
	imports: [
		ModalModule.forRoot(),
		BrowserAnimationsModule,
		ReactiveFormsModule,
		NoopAnimationsModule,
		NgxDatatableModule,
		CommonModule,
		HttpClientModule,
		FormsModule,
		AppRoutingModule,
		MaterialModule,
		FilterPipeModule,
		MatDialogModule
	]
})
export class AppModuleShared {
}