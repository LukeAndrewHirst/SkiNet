import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { TestErrorsComponent } from './features/test-errors/test-errors.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';
import { CartComponent } from './features/cart/cart.component';
import { LoginComponent } from './features/login/login.component';
import { RegisterComponent } from './features/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { OrdersComponent } from './features/orders.component';
import { OrdersDetailsComponent } from './features/orders-details/orders-details.component';
import { adminGuard } from './core/guards/admin.guard';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'shop', component: ShopComponent},
    {path: 'shop/:id', component: ProductDetailsComponent},
    {path: 'cart', component: CartComponent},
    {path: 'checkout', loadChildren: () => import('./features/checkout/routes').then(r => r.checkoutRoutes)},
    {path: 'orders', component: OrdersComponent, canActivate: [authGuard]},
    {path: 'orders/:id', component: OrdersDetailsComponent, canActivate: [authGuard]},
    {path: 'account/login', component: LoginComponent},
    {path: 'account/register', component: RegisterComponent},
    {path: 'admin', loadChildren: () => import('./features/admin/routes').then(r => r.adminRoutes), canActivate: [authGuard, adminGuard]},
    {path: 'test-errors', component: TestErrorsComponent},
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-error', component: ServerErrorComponent},
    {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];
