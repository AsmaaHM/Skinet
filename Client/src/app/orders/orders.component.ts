import { Component, OnInit } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { OrderService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders: IOrder[]; 
  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.getOrders(); 
  }


  getOrders() {

    this.orderService.getOrdersForUser()
    .subscribe(
      (response: IOrder[]) => this.orders = response, 
      (error) => console.log(error)
    )
  }
}
