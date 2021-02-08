import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  // input property is something we receive from parent component
  @Input() totalCount: number;
  @Input() pageSize: number;
  // output emits from child to parent component
  // we are emiting information out of the component, this will emit an event
  // we are specifying <> the type of thing we are going to emit
  // with output we want to call the onpagechanged method from shop.component.ts
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

onPagerChange(event: any) {
  // now we are emiting number from child component
    this.pageChanged.emit(event.page);
}

}










