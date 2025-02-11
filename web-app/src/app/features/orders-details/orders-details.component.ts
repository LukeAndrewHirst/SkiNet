import { Component, inject, OnInit } from '@angular/core';
import { OrderService } from '../../core/services/order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from '../../shared/models/order';
import { MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { AddressPipe } from '../../shared/pipes/address.pipe';
import { CardPipe } from '../../shared/pipes/card.pipe';
import { AccountService } from '../../core/services/account.service';
import { AdminService } from '../../core/services/admin.service';

@Component({
  selector: 'app-orders-details',
  imports: [MatCardModule, MatButton, DatePipe, CurrencyPipe, AddressPipe, CardPipe],
  templateUrl: './orders-details.component.html',
  styleUrl: './orders-details.component.scss'
})
export class OrdersDetailsComponent implements OnInit {
  private orderService = inject(OrderService);
  private accountService = inject(AccountService);
  private adminService = inject(AdminService);
  
  private activatedRoute = inject(ActivatedRoute);
  private router = inject(Router);

  order?: Order;
  buttonText = this.accountService.isAdmin() ? 'Return To Admin' : 'Return To Orders';

  ngOnInit(): void {
    this.loadOrder();
  }

  onReturnClick() {
    this.accountService.isAdmin() ? this.router.navigateByUrl('/admin') : this.router.navigateByUrl('/orders');
  }

  loadOrder() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(!id) return;

    const loadOrderData = this.accountService.isAdmin() ? this.adminService.getOrder(+id) : this.orderService.getOrderDetails(+id);

    loadOrderData.subscribe({
      next: order => this.order = order,
      error: error => console.log(error)
    })
  }
}
