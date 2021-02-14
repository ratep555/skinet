import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { IBasket, IBasketItem, Basket, IBasketTotals } from '../shared/models/basket';
import { map } from 'rxjs/operators';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  // Bsubject - Requires an initial value and emits the current value to new subscribers
  // you have to give initial value - null
  private basketSource = new BehaviorSubject<IBasket>(null);
  // $ means it is observable
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string) {
    return this.http.get(this.baseUrl + 'basket?id=' + id)
      .pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          this.calculateTotals();
        })
      );
  }

  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.calculateTotals();
    }, error => {
      console.log(error);
    });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);
    let basket = this.getCurrentBasketValue();
    if (basket === null) {
      basket = this.createBasket();
    }
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    // returns boolean, it shows whether there are any items that match this id
    if (basket.items.some(x => x.id === item.id)) {
      basket.items = basket.items.filter(i => i.id !== item.id);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        // now we have an empzty basket and we want to delete it
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = 0;
    // items is array of items which can have more then one in terms of quantity
    // you need to sum that and reduce that into single number, hover over reduce
    // b is item, a is result, number that we are returning
    // a is given the initial value of 0
    const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subtotal + shipping;
    // we are setting the next value of the observable
    this.basketTotalSource.next({shipping, total, subtotal});
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);
    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType
    };
  }
}