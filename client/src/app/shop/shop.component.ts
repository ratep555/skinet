import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopModule } from './shop.module';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  // with viewchild we are accessing ShopModule.html - search
  // static je false ako bismo recimo željeli imati pristup input search fieldu u .html a
  // to bi polje bilo recimo ograničeno sa ng-if glede svog pojavljivanja, lekcija 103
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams = new ShopParams();
  totalCount: number;


  // value is what we want to send as a query string parameter
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit() {
// this means we are accessing something available in this class
this.getProducts();
this.getBrands();
this.getTypes();
}

getProducts() {
    // we don't have to specify the type of response because
    // we are doing this in service
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      // response su podaci iz api
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
      });
  }

getBrands() {
this.shopService.getBrands().subscribe(response => {
  // ovime dodajemo još jedan brands imenom all, ... je spread operator, razmiče sve ostale, prvi postaje all
  this.brands = [{id: 0, name: 'All'}, ...response];
}, error => {
  console.log(error);
});
}

getTypes() {
this.shopService.getTypes().subscribe(response => {
  this.types = [{id: 0, name: 'All'}, ...response];
}, error => {
  console.log(error);
});
}

onBrandSelected(brandId: number) {
this.shopParams.brandId = brandId;
this.shopParams.pageNumber = 1;
this.getProducts();
}

onTypeSelected(typeId: number) {
this.shopParams.typeId = typeId;
this.getProducts();
}

onSortSelected(sort: string) {
  this.shopParams.sort = sort;
  this.getProducts();
}

onPageChanged(event: any) {
  if (this.shopParams.pageNumber !== event) {
    this.shopParams.pageNumber = event;
    this.getProducts();
  }
}

onSearch() {
  this.shopParams.search = this.searchTerm.nativeElement.value;
  this.shopParams.pageNumber = 1;
  this.getProducts();
}

// this will reset the search term
onReset() {
  this.searchTerm.nativeElement.value = '';
  // we are reseting all of our filters
  this.shopParams = new ShopParams();
  this.getProducts();
}
}







