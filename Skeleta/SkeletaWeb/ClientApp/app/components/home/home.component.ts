import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { startWith, map } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';

export class Country {
	constructor(public name: string, public population: string, public flag: string) { }
}

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

	constructor(private _formBuilder: FormBuilder) {
		this.countryCtrl = new FormControl();
		this.filteredCountries = this.countryCtrl.valueChanges
			.pipe(
			startWith(''),
			map(country => country ? this.filtercountrys(country) : this.countrys.slice())
			);
	}

	filtercountrys(name: string) {
		return this.countrys.filter(country =>
			country.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
	}

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

	countryCtrl: FormControl;
	filteredCountries: Observable<any[]>;

	countrys: Country[] = [
		{
			name: 'Hungary',
			population: '9.818M',			
			flag: 'https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Flag_of_Hungary.svg/125px-Flag_of_Hungary.svg.png'
		},
		{
			name: 'England',
			population: '53.01M',
			flag: 'https://upload.wikimedia.org/wikipedia/en/thumb/b/be/Flag_of_England.svg/125px-Flag_of_England.svg.png'
		},
		{
			name: 'USA',
			population: '323.1M',
			flag: 'https://upload.wikimedia.org/wikipedia/en/thumb/a/a4/Flag_of_the_United_States.svg/125px-Flag_of_the_United_States.svg.png'
		},
		{
			name: 'China',
			population: '1412.6M',
			flag: 'https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Flag_of_the_People%27s_Republic_of_China.svg/125px-Flag_of_the_People%27s_Republic_of_China.svg.png'
		}
	];
}