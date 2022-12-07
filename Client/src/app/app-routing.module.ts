import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { skip } from 'rxjs/operators';
import { AuthGuard } from './core/guards/auth.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home/home.component';

const routes: Routes = [
  {path: '', component : HomeComponent},
  {path: 'test-error', component : TestErrorComponent, data: {breadcrum: 'Test Errors'}},
  {path: 'not-found', component : NotFoundComponent, data: {breadcrum: 'Server Error'}},
  {path: 'server-error', component : ServerErrorComponent, data: {breadcrum: 'Not found'}},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod=> mod.ShopModule), data: {breadcrum: 'Shop'}},
  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(mod=> mod.BasketModule), data: {breadcrum: 'Basket'}},
  {path: 'checkout', 
  loadChildren: () => import('./checkout/checkout.module').then(mod=> mod.CheckoutModule), 
  data: {breadcrum: 'Checkout'}, 
  canActivate: [AuthGuard] },
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod=> mod.AccountModule), data: {breadcrum: {skip: true}}},
  {path: '*',  redirectTo: 'not-found' , pathMatch: 'full'}, 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
