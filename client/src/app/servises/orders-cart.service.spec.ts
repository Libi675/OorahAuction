import { TestBed } from '@angular/core/testing';

import { OrdersCartService } from './orders-cart.service';

describe('OrdersCartService', () => {
  let service: OrdersCartService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrdersCartService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
