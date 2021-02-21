import { Component, OnInit, Input } from '@angular/core';
import { CdkStepper } from '@angular/cdk/stepper';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  providers: [{provide: CdkStepper, useExisting: StepperComponent}]
})

// sa extends koristiš cdksteppera
export class StepperComponent extends CdkStepper implements OnInit {
  @Input() linearModeSelected: boolean;

  ngOnInit() {
    // hover over linear
    this.linear = this.linearModeSelected;
  }

  onClick(index: number) {
    // hover over, we are tracking on which step we are
    this.selectedIndex = index;
  }

}
