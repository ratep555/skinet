import { Component, OnInit, ViewChild, ElementRef, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
// ovo dolje su metode koje su implementirane kao interfejsi iz textinputcomponenta
// zato što smo mu dodali controlvalueaccessora koji nam omogućuje formcontrols validation, hover over cvaccessor
export class TextInputComponent implements OnInit, ControlValueAccessor {
  // sa viewchild imamo access do nativeelementa - inputa
  @ViewChild('input', { static: true }) input: ElementRef;
  @Input() type = 'text';
  @Input() label: string;

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit() {
    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];
    // asyncvalidator comes after synchronous, for example if we want to check whether something went to api
    const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

    control.setValidators(validators);
    control.setAsyncValidators(asyncValidators);
    control.updateValueAndValidity();
  }

  onChange(event) { }

  onTouched() { }

  // we need to get the value of the input and write it into this
  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  registerOnChange(fn: any): void {
    // onchange is equal to controlvalueaccessor function - fn
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

}
