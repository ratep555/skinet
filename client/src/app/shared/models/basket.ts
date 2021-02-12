/* import * as uuid from 'uuid';
 */
import { v4 as uuidv4 } from 'uuid';

/* import { uuid } from 'uuidv4';
 */
export interface IBasket {
    id: string;
    items: IBasketItem[];
}

export interface IBasketItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}
// when we create a new basket, we want to give it a unique identifier
// the easiest way to do it is to create class for our basket
export class Basket implements IBasket {
/*     id = uuid.v4();
 */  id = uuidv4();
 // we need to set this to an empty array in order to avoid undefined message
    items: IBasketItem[] = [];
}

export interface IBasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
}

