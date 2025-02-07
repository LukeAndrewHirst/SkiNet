import { Component, inject, OnInit } from '@angular/core';
import { OrderService } from '../../core/services/order.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Order } from '../../shared/models/order';
import { MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { AddressPipe } from '../../shared/pipes/address.pipe';
import { CardPipe } from '../../shared/pipes/card.pipe';

@Component({
  selector: 'app-orders-details',
  imports: [MatCardModule, MatButton, DatePipe, CurrencyPipe, AddressPipe, CardPipe, RouterLink],
  templateUrl: './orders-details.component.html',
  styleUrl: './orders-details.component.scss'
})
export class OrdersDetailsComponent implements OnInit {
  private orderService = inject(OrderService);

  private activatedRoute = inject(ActivatedRoute);

  order?: Order;

  ngOnInit(): void {
    this.loadOrder();
  }

  loadOrder() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(!id) return;

    this.orderService.getOrderDetails(+id).subscribe({
      next: order => this.order = order,
      error: error => console.log(error)
    })
  }
}
