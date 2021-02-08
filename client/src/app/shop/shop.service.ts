import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import {map} from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    // we are using params object
    // we use let if we want to reasign something
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      // we are passing name (key) and value, it has to be tostring since we are passing this
      // as a query string parameter
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());


    // here we are passing parameters
    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
    // pipe is a wrapper around any rxjs operator we want to use (map is rxjs)
    // it allows us to chain multiple rxjs operators together to do something with the observable
    .pipe(
      // we are taking response from the api
      map(response => {
        // we are extracting response body
         return response.body;
      })
    );
  }

  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }
  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }

}






