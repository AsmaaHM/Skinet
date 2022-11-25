import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
  {path: '*',  redirectTo: 'not-found' , pathMatch: 'full'}, 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
