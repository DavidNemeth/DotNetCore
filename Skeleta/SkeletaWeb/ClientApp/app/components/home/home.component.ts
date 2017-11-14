import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";

@Component({
	selector: 'home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})
export class HomeComponent {
	step = 0;

	setStep(index: number) {
		this.step = index;
	}

	nextStep() {
		this.step++;
	}

	prevStep() {
		this.step--;
	}

	//Stepper
	isLinear = false;
	firstFormGroup: FormGroup;
	secondFormGroup: FormGroup;
	animalFormGroup: FormGroup;
	ToggleLinear(): void{
		this.isLinear = !this.isLinear;
	}

	constructor(private _formBuilder: FormBuilder) { }

	ngOnInit() {
		this.firstFormGroup = this._formBuilder.group({
			firstCtrl: ['', Validators.required]			
		});
		this.secondFormGroup = this._formBuilder.group({
			secondCtrl: ['', Validators.required]
		});		
	}

	//selectlist
	animalControl = new FormControl('', [Validators.required]);

	animals = [
		{ name: 'Dog', sound: 'Woof!' },
		{ name: 'Cat', sound: 'Meow!' },
		{ name: 'Cow', sound: 'Moo!' },
		{ name: 'Fox', sound: 'Wa-pa-pa-pa-pa-pa-pow!' },
	];
}