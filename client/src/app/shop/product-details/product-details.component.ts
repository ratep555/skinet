import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService)
  { this.bcService.set('@productDetails', '');
}

  ngOnInit(): void {
this.loadProduct();
  }

  loadProduct() {
    // id is id defined in app-routing.module - shop/:id
    // morali smo staviti + na početku jer želimo tostring
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product;
      // with @ we are accessing alias fromshoprouting.module.ts
      this.bcService.set('@productDetails', product.name);
    }, error => {
      console.log(error);
    });
  }
 /*  loadProduct(id: number) {
    return this.shopService.getProduct(this.product.id);
  } */

}
